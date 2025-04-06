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
    private byte[]? xlsxFile { get; set; }

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
        
        if (studentList == null)
        {
            await Notificator.ShowError("Error: Lista de estudiante nula.");
            return;
        }
        
        foreach (var student in studentList)
        {
            if (!await CheckStudentGrades(student))
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
    private async Task DownloadXlsx()
    {
        await controller.GetGradeDocument(TeacherId, EnrollmentId, currentPartial, enrollmentLabel);
    }

    private async Task LoadXlsxFile(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file.Name.EndsWith(".xlsx"))
        {
            await using var stream = file.OpenReadStream();
            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);

            xlsxFile = memoryStream.ToArray();
        }
        else
        {
            await Notificator.ShowError("Por favor, selecciona un archivo Excel válido.");
        }
    }

    private async Task UpdateGradesFromXlsxFile()
    {
        if (xlsxFile == null)
        {
            await Notificator.ShowError("No se ha cargado ningún archivo.");
            return;
        }

        var Option = await Notificator.ShowAlertQuestion("Advertencia",
            "Al actualizar las calificaciones desde este archivo:\n\n Se sobrescribirán todas las calificaciones registradas previamente para los estudiantes en las asignaturas impartidas por usted.\n\n Los cambios no se pueden deshacer automáticamente. ¿Desea continuar?",
            ("Sí, Seguro", "No, cancelar"));

        if (!Option) return;

        try
        {
            await LoadGradesFromXlsxFile();
        }
        catch (Exception ex)
        {
            await Notificator.ShowError($"Error al procesar el archivo: {ex.Message}");
        }
    }

    private async Task LoadGradesFromXlsxFile()
    {
        using (var stream = new MemoryStream(xlsxFile!))
        {
            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);

            // Configuración de posiciones
            const int primeraFilaDatos = 11;
            const int filaIdsAsignaturas = 9;
            const int primeraColumnaAsignatura = 6;

            // Validar archivo del teacher
            var valorCeldaA1 = worksheet.Cell("D9").Value.ToString();
            if (!valorCeldaA1.Equals(TeacherId, StringComparison.OrdinalIgnoreCase))
            {
                await Notificator.ShowError("El archivo seleccionado no es el correcto.");
                xlsxFile = null;
                return;
            }

            // Identificar columnas
            var colAsignatura = primeraColumnaAsignatura;
            while (!worksheet.Cell(filaIdsAsignaturas, colAsignatura).IsEmpty())
                colAsignatura++;

            var columnaConducta = colAsignatura;
            var columnaCodigo = columnaConducta + 1;

            // Leer asignaturas
            var subjectIdDictionary = new Dictionary<int, string>();
            for (var col = primeraColumnaAsignatura; col <= columnaConducta; col++)
            {
                subjectIdDictionary[col] = worksheet.Cell(filaIdsAsignaturas, col).Value.ToString();
            }

            // Procesar estudiantes
            var fila = primeraFilaDatos;
            while (!worksheet.Cell(fila, columnaCodigo).IsEmpty())
            {
                var studentId = worksheet.Cell(fila, columnaCodigo).Value.ToString();
                var student = studentList!.FirstOrDefault(s => s.studentId == studentId);

                if (student == null)
                {
                    fila++;
                    continue;
                }

                // Procesar asignaturas
                foreach (var (column, subjectId) in subjectIdDictionary)
                {
                    var celdaNota = worksheet.Cell(fila, column);
                    var nombreAsignatura = worksheet.Cell(10, column).Value.ToString(); // Fila 10 tiene nombres

                    if (!celdaNota.TryGetValue(out decimal notaDecimal))
                    {
                        var valorCelda = celdaNota.Value.ToString();
                        valorCelda = valorCelda.Replace(",", ".");

                        if (!decimal.TryParse(valorCelda, NumberStyles.Any, CultureInfo.InvariantCulture,
                                out notaDecimal))
                        {
                            await Notificator.ShowError($"Valor no numérico en {nombreAsignatura} (Fila {fila})");
                            xlsxFile = null;
                            return;
                        }
                    }

                    if (!GradeEntity.IsValidGradeValue(notaDecimal))
                    {
                        await Notificator.ShowError(
                            $"Nota inválida ({notaDecimal}) en {nombreAsignatura} (Fila {fila}). Debe ser entre 0 y 100.");
                        xlsxFile = null;
                        return;
                    }

                    var grade = student.gradeList!.FirstOrDefault(g => g.subjectId == subjectId);
                    if (grade != null)
                    {
                        grade.grade = notaDecimal;
                    }
                }

                // Procesar conducta
                var celdaConducta = worksheet.Cell(fila, columnaConducta);
                if (celdaConducta.TryGetValue(out decimal conductaDecimal) ||
                    decimal.TryParse(celdaConducta.Value.ToString().Replace(",", "."),
                        NumberStyles.Any, CultureInfo.InvariantCulture, out conductaDecimal))
                {
                    if (GradeEntity.IsValidGradeValue(conductaDecimal))
                    {
                        student.conductGrade = conductaDecimal;
                    }
                    else
                    {
                        await Notificator.ShowError(
                            $"Conducta inválida ({conductaDecimal}) en Fila {fila}. Debe ser entre 0 y 100.");
                        xlsxFile = null;
                        return;
                    }
                }
                else if (!celdaConducta.IsEmpty())
                {
                    await Notificator.ShowError($"Valor de conducta no numérico en Fila {fila}");
                    xlsxFile = null;
                    return;
                }

                fila++;
            }
        }

        await UpdateGradeList();
        xlsxFile = null;
    }

    //Utilities Method
    protected override bool IsLoading()
    {
        return partialList == null;
    }

    private static string GetDatetime()
    {
        return DateTime.UtcNow.toStringUtc6(true);
    }

    private string GetPartialName()
    {
        var activePartial = partialList!.FirstOrDefault(e => e.partialId == currentPartial);
        return activePartial != null ? activePartial.label : "No hay parcial activo";
    }

    private async Task<bool> CheckStudentGrades(StudentEntity student)
    {
        // Lista para acumular todos los errores encontrados
        var errorList = new List<string>();

        // Validación de la lista de calificaciones
        if (student.gradeList == null)
        {
            errorList.Add("La lista de calificaciones (gradeList) es nula");
        }
        else if (!student.gradeList.Any())
        {
            errorList.Add("La lista de calificaciones está vacía");
        }
        else
        {
            // Validar cada calificación individual
            foreach (var grade in student.gradeList)
            {
                // Validación del objeto GradeEntity
                if (string.IsNullOrWhiteSpace(grade.studentId))
                    errorList.Add($"Calificación sin studentId (Asignatura: {grade.subjectId})");

                if (string.IsNullOrWhiteSpace(grade.subjectId))
                    errorList.Add($"Calificación sin subjectId (Estudiante: {grade.studentId})");

                if (!grade.HasValidGrade())
                    errorList.Add($"Calificación {grade.grade} fuera de rango (0-100) en {grade.subjectId}");

                if (!grade.HasValidConductGrade())
                    errorList.Add($"Nota de conducta {grade.conductGrade} fuera de rango (0-100) en {grade.subjectId}");

                if (string.IsNullOrWhiteSpace(grade.label))
                    errorList.Add($"Falta label en la asignatura {grade.subjectId}");
            }
        }

        if (!errorList.Any())
        {
            return true;
        }
        
        // Mostrar todos los errores encontrados
        var errorMessage = new StringBuilder();
        errorMessage.AppendLine($"Errores encontrados para el estudiante ID: {student.studentId}");

        foreach (var error in errorList.Distinct())
        {
            errorMessage.AppendLine($"- {error}");
        }

        await Notificator.ShowError(errorMessage.ToString());
        return false;
    }
}