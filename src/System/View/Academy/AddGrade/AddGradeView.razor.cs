using System.Globalization;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.AddGrade;

public partial class AddGradeView : BaseView
{
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private AddingStudentGradesController controller { get; set; } = null!;

    private int currentPartial { get; set; }
    private int ActiveTabId { get; set; } = 1;
    private string TeacherId { get; set; } = null!;
    private string? TeacherName { get; set; } = string.Empty;
    [Parameter] public string EnrollmentId { get; set; } = null!;
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

    protected override bool IsLoading()
    {
        return partialList == null;
    }

    private static string GetDatetime()
    {
        return DateTime.Now.ToString("dddd dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
    }

    private string getPartialName()
    {
        var activePartial = partialList!.FirstOrDefault(e => e.partialId == currentPartial);
        return activePartial != null ? activePartial.label : "No hay parcial activo";
    }
}