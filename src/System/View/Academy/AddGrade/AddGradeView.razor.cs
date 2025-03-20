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
    private byte[] GenerarExcel()
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Calificaciones");

            // Encabezados
            worksheet.Cell(1, 1).Value = "Código";
            worksheet.Cell(1, 2).Value = "Estudiante";

            // Encabezados de materias
            int columna = 3;
            foreach (var subject in subjectList!)
            {
                worksheet.Cell(1, columna).Value = subject.initials;
                worksheet.Cell(1, columna).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, columna).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                worksheet.Cell(1, columna).Style.Font.Bold = true;
                worksheet.Cell(1, columna).Style.Fill.BackgroundColor = XLColor.LightGray;

                worksheet.Cell(2, columna).Value = "Clave";
                worksheet.Cell(2, columna + 1).Value = "Valor";
                columna += 2;
            }

            // Encabezado de conducta
            worksheet.Cell(1, columna).Value = "Conducta";
            worksheet.Cell(1, columna).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(1, columna).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Cell(1, columna).Style.Font.Bold = true;
            worksheet.Cell(1, columna).Style.Fill.BackgroundColor = XLColor.LightGray;

            // Datos de estudiantes
            int fila = 3;
            foreach (var student in studentList!.OrderBy(s => s.sex).ThenBy(s => s.fullName))
            {
                worksheet.Cell(fila, 1).Value = student.studentId;
                worksheet.Cell(fila, 2).Value = student.fullName;

                columna = 3;
                foreach (var subject in subjectList!)
                {
                    var grade = student.gradeList!.FirstOrDefault(e => e.subjectId == subject.subjectId);
                    if (grade != null)
                    {
                        worksheet.Cell(fila, columna).Value = subject.subjectId;
                        worksheet.Cell(fila, columna + 1).Value = grade.grade;
                    }
                    else
                    {
                        worksheet.Cell(fila, columna).Value = "No asignado";
                        worksheet.Cell(fila, columna + 1).Value = "No asignado";
                    }

                    columna += 2;
                }

                // Conducta
                worksheet.Cell(fila, columna).Value = student.conductGrade;
                fila++;
            }
            
            worksheet.Columns().AdjustToContents();
            
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return stream.ToArray();
            }
        }
    }
    private void DescargarExcel()
    {
        var contenidoExcel = GenerarExcel();
        
        var nombreArchivo = $"Calificaciones_{GetPartialName()}_{DateTime.Now:yyyyMMdd}.xlsx";
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var base64Contenido = Convert.ToBase64String(contenidoExcel);
        
        Navigator.ToPage($"data:{contentType};base64,{base64Contenido}");
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
        if (archivoExcel == null)
        {
            await Notificator.ShowError("No se ha cargado ningún archivo.");
            return;
        }

        using (var stream = new MemoryStream(archivoExcel))
        {
            var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheet(1);
            
            var fila = 3;
            while (!worksheet.Cell(fila, 1).IsEmpty())
            {
                var codigoEstudiante = worksheet.Cell(fila, 1).Value.ToString();
                var estudiante = studentList!.FirstOrDefault(s => s.studentId == codigoEstudiante);

                if (estudiante != null)
                {
                    int columna = 3;
                    foreach (var subject in subjectList!)
                    {
                        var clave = worksheet.Cell(fila, columna).Value.ToString();
                        var valor = worksheet.Cell(fila, columna + 1).Value.ToString();

                        var grade = estudiante.gradeList!.FirstOrDefault(e => e.subjectId == clave);
                        if (grade != null)
                        {
                            grade.grade = int.Parse(valor);
                        }

                        columna += 2;
                    }
                    
                    estudiante.conductGrade = int.Parse(worksheet.Cell(fila, columna).Value.ToString());
                }

                fila++;
            }
        }
        
        await UpdateGradeList();
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