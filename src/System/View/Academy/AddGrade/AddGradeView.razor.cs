using System.Globalization;
using System.Text;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.AddGrade;

public partial class AddGradeView : BaseView
{
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private AddingStudentGradesController controller { get; set; } = null!;


    [Parameter] public string EnrollmentId { get; set; } = null!;
    private int currentPartial { get; set; }
    private int ActiveTabId { get; set; } = 1;
    private string TeacherId { get; set; } = null!;
    private string? TeacherName { get; set; } = string.Empty;
    private string enrollmentLabel { get; set; } = null!;

    private List<PartialEntity>? partialList { get; set; }
    private List<SubjectEntity>? subjectList { get; set; }
    private List<StudentEntity>? studentList { get; set; } = [];
    private byte[]? archivoExcel;

    protected override async Task OnParametersSetAsync()
    {
        partialList = await controller.GetPartialList();
        LoadActivePartial();
        await LoadTeacherInformation();
        await GetEnrollmentData();
    }

    private void LoadActivePartial()
    {
        var activePartial = partialList!.FirstOrDefault(t => t.isActive);
        currentPartial = activePartial?.partialId ?? partialList!.First().partialId;
        ActiveTabId = currentPartial;
    }

    private async Task LoadTeacherInformation()
    {
        TeacherId = await controller.GetTeacherId();
        var teacher = await controller.GetTeacherById(TeacherId);
        TeacherName = teacher.fullName;
    }

    private async Task GetEnrollmentData()
    {
        var result = await controller.GetEnrollment(TeacherId, EnrollmentId, currentPartial);
        enrollmentLabel = result.label;
        subjectList = result.subjectList;
        studentList = result.studentList;
    }

    private async Task UpdateGradeList()
    {
        var gradeList = new List<GradeEntity>();
        foreach (var student in studentList!)
        {
            if (!await ValidarEstudianteYCalificaciones(student))
            {
                return; 
            }
            
            student.UpdateConductGradeIntoList();
            gradeList.AddRange(student.gradeList!);
        }
        
        var response = await controller.UpdateGrade(TeacherId, EnrollmentId, currentPartial, gradeList);
        if (response)
        {
            await Notificator.ShowSuccess("Las calificaciones se han registrado satisfactoriamente.");
            await GetEnrollmentData();
            return;
        }

        await Notificator.ShowError("Hubo un fallo al intentar registrar las calificaciones.");
    }

    //Excels Method
    private async Task DescargarExcel()
    {
        await controller.GetGradeDocument(TeacherId, EnrollmentId, currentPartial, enrollmentLabel);
    }

    private async Task CargarArchivoExcel(InputFileChangeEventArgs e)
    {
        var archivo = e.File;
        if (archivo != null && archivo.Name.EndsWith(".xlsx"))
        {
            using (var stream = archivo.OpenReadStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memoryStream);
                    archivoExcel = memoryStream.ToArray();
                }
            }
        }
        else
        {
            await Notificator.ShowError("Por favor, selecciona un archivo Excel válido.");
        }
    }

    private async Task ActualizarDatosDesdeExcel()
    {
        var respon = await Notificator.ShowAlertQuestion("Advertencia",
            "Al actualizar las calificaciones desde este archivo:\n\n Se sobrescribirán todas las calificaciones registradas previamente para los estudiantes en las asignaturas impartidas por usted.\n\n Los cambios no se pueden deshacer automáticamente. ¿Desea continuar?",
            ("Si, Seguro", "No, cancelar"));

        if (!respon) return;

        if (archivoExcel == null)
        {
            await Notificator.ShowError("No se ha cargado ningún archivo.");
            return;
        }

        try
        {
            using (var stream = new MemoryStream(archivoExcel))
            {
                var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);

                // Configuración de posiciones
                const int primeraFilaDatos = 11;
                const int filaIdsAsignaturas = 9;
                const int primeraColumnaAsignatura = 6;

                // Validar archivo del teacher
                string valorCeldaA1 = worksheet.Cell("D9").Value.ToString();
                if (!valorCeldaA1.Equals(TeacherId, StringComparison.OrdinalIgnoreCase))
                {
                    await Notificator.ShowError("El archivo seleccionado no es el correcto.");
                    archivoExcel = null;
                    return;
                }

                // Identificar columnas
                int colAsignatura = primeraColumnaAsignatura;
                while (!worksheet.Cell(filaIdsAsignaturas, colAsignatura).IsEmpty())
                    colAsignatura++;

                int columnaConducta = colAsignatura;
                int columnaCodigo = columnaConducta + 1;

                // Leer asignaturas
                var asignaturas = new Dictionary<int, string>();
                for (int col = primeraColumnaAsignatura; col <= columnaConducta; col++)
                {
                    asignaturas[col] = worksheet.Cell(filaIdsAsignaturas, col).Value.ToString();
                }

                // Procesar estudiantes
                int fila = primeraFilaDatos;
                while (!worksheet.Cell(fila, columnaCodigo).IsEmpty())
                {
                    var codigoEstudiante = worksheet.Cell(fila, columnaCodigo).Value.ToString();
                    var estudiante = studentList!.FirstOrDefault(s => s.studentId == codigoEstudiante);

                    if (estudiante == null)
                    {
                        fila++;
                        continue;
                    }

                    // Procesar asignaturas
                    foreach (var (columna, idAsignatura) in asignaturas)
                    {
                        var celdaNota = worksheet.Cell(fila, columna);
                        string nombreAsignatura = worksheet.Cell(10, columna).Value.ToString(); // Fila 10 tiene nombres

                        if (!celdaNota.TryGetValue(out double notaDecimal))
                        {
                            string valorCelda = celdaNota.Value.ToString();
                            valorCelda = valorCelda.Replace(",", ".");

                            if (!double.TryParse(valorCelda, NumberStyles.Any, CultureInfo.InvariantCulture,
                                    out notaDecimal))
                            {
                                await Notificator.ShowError($"Valor no numérico en {nombreAsignatura} (Fila {fila})");
                                archivoExcel = null;
                                return;
                            }
                        }

                        if (notaDecimal < 0 || notaDecimal > 100)
                        {
                            await Notificator.ShowError(
                                $"Nota inválida ({notaDecimal}) en {nombreAsignatura} (Fila {fila}). Debe ser entre 0 y 100.");
                            archivoExcel = null;
                            return;
                        }

                        var grade = estudiante.gradeList!.FirstOrDefault(g => g.subjectId == idAsignatura);
                        if (grade != null)
                        {
                            grade.grade = (decimal)Math.Round(notaDecimal, 1);
                        }
                    }

                    // Procesar conducta
                    var celdaConducta = worksheet.Cell(fila, columnaConducta);
                    if (celdaConducta.TryGetValue(out double conductaDecimal) ||
                        double.TryParse(celdaConducta.Value.ToString().Replace(",", "."),
                            NumberStyles.Any, CultureInfo.InvariantCulture, out conductaDecimal))
                    {
                        if (conductaDecimal >= 0 && conductaDecimal <= 100)
                        {
                            estudiante.conductGrade = (decimal)Math.Round(conductaDecimal, 1);
                        }
                        else
                        {
                            await Notificator.ShowError(
                                $"Conducta inválida ({conductaDecimal}) en Fila {fila}. Debe ser entre 0 y 100.");
                            archivoExcel = null;
                            return;
                        }
                    }
                    else if (!celdaConducta.IsEmpty())
                    {
                        await Notificator.ShowError($"Valor de conducta no numérico en Fila {fila}");
                        archivoExcel = null;
                        return;
                    }

                    fila++;
                }
            }

            await UpdateGradeList();
            archivoExcel = null;
        }
        catch (Exception ex)
        {
            await Notificator.ShowError($"Error al procesar el archivo: {ex.Message}");
        }
    }

    //Utilities Method
    protected override bool IsLoading()
    {
        return partialList == null;
    }

    private static string GetDatetime()
    {
        return DateTime.Now.ToString("dddd dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
    }

    private string GetPartialName()
    {
        var activePartial = partialList!.FirstOrDefault(e => e.partialId == currentPartial);
        return activePartial != null ? activePartial.label : "No hay parcial activo";
    }

    private async Task<bool> ValidarEstudianteYCalificaciones(StudentEntity student)
    {
        // Lista para acumular todos los errores encontrados
        var errores = new List<string>();

        if (student == null)
        {
            await Notificator.ShowError("Error: Referencia de estudiante nula.");
            return false;
        }

        // Validación de la lista de calificaciones
        if (student.gradeList == null)
        {
            errores.Add("La lista de calificaciones (gradeList) es nula");
        }
        else if (!student.gradeList.Any())
        {
            errores.Add("La lista de calificaciones está vacía");
        }
        else
        {
            // Validar cada calificación individual
            foreach (var grade in student.gradeList)
            {
                if (grade == null)
                {
                    errores.Add("Se encontró una calificación nula en la lista");
                    continue;
                }

                // Validación del objeto GradeEntity
                if (string.IsNullOrWhiteSpace(grade.studentId))
                    errores.Add($"Calificación sin studentId (Asignatura: {grade.subjectId})");

                if (string.IsNullOrWhiteSpace(grade.subjectId))
                    errores.Add($"Calificación sin subjectId (Estudiante: {grade.studentId})");

                if (grade.grade < 0 || grade.grade > 100)
                    errores.Add($"Calificación {grade.grade} fuera de rango (0-100) en {grade.subjectId}");

                if (grade.conductGrade < 0 || grade.conductGrade > 100)
                    errores.Add($"Nota de conducta {grade.conductGrade} fuera de rango (0-100) en {grade.subjectId}");

                if (string.IsNullOrWhiteSpace(grade.label))
                    errores.Add($"Falta label en la asignatura {grade.subjectId}");
            }
        }

        // Mostrar todos los errores encontrados
        if (errores.Any())
        {
            var mensajeError = new StringBuilder();
            mensajeError.AppendLine($"Errores encontrados para el estudiante ID: {student.studentId}");

            foreach (var error in errores.Distinct())
            {
                mensajeError.AppendLine($"- {error}");
            }

            await Notificator.ShowError(mensajeError.ToString());
            return false;
        }

        return true;
    }
}