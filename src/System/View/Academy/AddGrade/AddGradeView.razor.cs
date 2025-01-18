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
    [Inject] private AddStudentGradeController controller { get; set; } = null!;

    private int currentPartial { get; set; }
    private int ActiveTabId { get; set; } = 1;
    private string TeacherId { get; set; } = null!;
    private string? TeacherName { get; set; } = string.Empty;
    [Parameter] public string EnrollmentId { get; set; } = null!;
    private string enrollmentLabel { get; set; } = null!;

    private List<PartialEntity>? partialsList { get; set; }
    private List<SubjectEntity>? subjectList { get; set; }
    private List<StudentEntity>? studentList { get; set; } = [];

    protected override async Task OnParametersSetAsync()
    {
        partialsList = await controller.GetPartialsList();
        LoadActivePartial();
        await LoadTeacherInformation();

        await GetEnrollmentData();
    }

    private void LoadActivePartial()
    {
        var activePartial = partialsList!.FirstOrDefault(t => t.isActive);
        currentPartial = activePartial?.partialId ?? 1;
        ActiveTabId = currentPartial;
    }

    private async Task LoadTeacherInformation()
    {
        TeacherId = await controller.getTeacherId();
        TeacherName = await controller.getTeacherName(TeacherId);
    }

    private async Task GetEnrollmentData()
    {
        var result = await controller.GetFullEnrollment(getRequestDto());
        
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

            student.setConductGrade();
            gradeList.AddRange(student.gradeList);
        }

        var response = await controller.UpdateGrade(getRequestDto(), gradeList);
        if (response)
        {
            await Notificator.ShowSuccess("Las calificaciones se han registrado satisfactoriamente.");
            await GetEnrollmentData();
            return;
        }

        await Notificator.ShowError("Hubo un fallo al intentar registrar las calificaciones.");
    }

    private TeacherEnrollmentByPartialDto getRequestDto()
    {
        return new TeacherEnrollmentByPartialDto
        {
            teacherId = TeacherId,
            enrollmentId = EnrollmentId,
            partialId = currentPartial
        };
    }

    protected override bool IsLoading()
    {
        return partialsList == null;
    }

    private string getDatetime()
    {
        return DateTime.Now.ToString("dddd dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
    }

    private string getPartialName()
    {
        return currentPartial switch
        {
            1 => "I Parcial",
            2 => "II Parcial",
            3 => "III Parcial",
            4 => "VI Parcial",
            _ => "No hay parcial activo."
        };
    }
}