using System.Globalization;
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
        if (studentList == null || studentList.Count == 0)
        {
            await Notificator.ShowError("No hay estudiantes para guardar las calificaciones.");
            return;
        }

        var gradeList = new List<GradeEntity>();
        foreach (var student in studentList)
        {
            if (student.gradeList == null)
            {
                await Notificator.ShowError($"No hay calificaciones para el estudiante {student.fullName}.");
                return;
            }

            student.UpdateConductGradeIntoList();
            gradeList.AddRange(student.gradeList);
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

    private byte[]? archivoExcel;

    //Excels Method
    private async Task DescargarExcel()
    {
        await controller.GetGradeDocument(TeacherId, EnrollmentId, currentPartial);
    }

    //--------------------------------
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

        if (respon)
        {
            if (archivoExcel == null)
            {
                await Notificator.ShowError("No se ha cargado ningún archivo.");
                return;
            }

            using (var stream = new MemoryStream(archivoExcel))
            {
                var workbook = new XLWorkbook(stream);
                var worksheet = workbook.Worksheet(1);

                // Configuración de posiciones basadas en la estructura
                const int primeraFilaDatos = 9; // Estudiantes desde fila 9
                const int filaIdsAsignaturas = 7; // IDs en fila 7
                const int columnaCodigo = 3; // Código en columna C (ignorando A vacía y B)
                const int primeraColumnaAsignatura = 6; // Primera asignatura en columna F

                // Leer todas las asignaturas disponibles primero
                var asignaturas = new Dictionary<int, string>();
                int colAsignatura = primeraColumnaAsignatura;
                while (!worksheet.Cell(filaIdsAsignaturas, colAsignatura).IsEmpty())
                {
                    var idAsignatura = worksheet.Cell(filaIdsAsignaturas, colAsignatura).Value.ToString();
                    asignaturas[colAsignatura] = idAsignatura;
                    colAsignatura++;
                }

                // Procesar cada estudiante
                int fila = primeraFilaDatos;
                while (!worksheet.Cell(fila, columnaCodigo).IsEmpty())
                {
                    var codigoEstudiante = worksheet.Cell(fila, columnaCodigo).Value.ToString();
                    var estudiante = studentList!.FirstOrDefault(s => s.studentId == codigoEstudiante);

                    if (estudiante != null)
                    {
                        // Procesar cada asignatura
                        foreach (var (columna, idAsignatura) in asignaturas)
                        {
                            var notaValue = worksheet.Cell(fila, columna).Value.ToString();
                            if (int.TryParse(notaValue, out int nota))
                            {
                                var grade = estudiante.gradeList!.FirstOrDefault(g => g.subjectId == idAsignatura);
                                if (grade != null) grade.grade = Math.Max(0, nota);
                            }
                        }

                        // Procesar conducta (última columna)
                        var conductaCol = asignaturas.Keys.Last() + 1;
                        estudiante.conductGrade = int.TryParse(
                            worksheet.Cell(fila, conductaCol).Value.ToString(),
                            out int conducta)
                            ? conducta
                            : 0;
                    }

                    fila++;
                }
            }

            await UpdateGradeList();
            archivoExcel = null;
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
}