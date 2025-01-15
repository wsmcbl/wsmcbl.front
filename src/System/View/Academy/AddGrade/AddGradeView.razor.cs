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
    public string enrollmentLabel { get; set; } = null!;

    private List<PartialEntity>? partialsList { get; set; }
    private List<SubjectEntity>? subjectList { get; set; }
    private List<StudentEntity>? studentList { get; set; } = [];

    protected override async Task OnParametersSetAsync()
    {
        partialsList = await controller.GetPartialsList();
        loadActivePartial();
        await loadTeacherInformation();

        await GetFullInfoEnrollment();
    }

    private void loadActivePartial()
    {
        var activePartial = partialsList!.FirstOrDefault(t => t.isActive);
        currentPartial = activePartial?.partialId ?? 1;
        ActiveTabId = currentPartial;
    }

    private async Task loadTeacherInformation()
    {
        TeacherId = await controller.getTeacherId();
        TeacherName = await controller.getTeacherName(TeacherId);
    }

    private async Task GetFullInfoEnrollment()
    {
        var result = await controller.GetFullEnrollment(getTeacherEnrollmentByPartialDto());

        subjectList = result.subjectList;
        studentList = result.studentList;
    }

    private async Task ToSaveGrade()
    {
        if (studentList == null || studentList.Count == 0)
        {
            await Notificator.ShowError("Error", "No hay estudiantes para guardar las calificaciones.");
            return;
        }

        var gradeList = new List<GradeEntity>();
        foreach (var student in studentList)
        {
            if (student.gradeList == null)
            {
                await Notificator.ShowError("Error", $"No hay calificaciones para el estudiante {student.fullName}.");
                return;
            }

            gradeList.AddRange(student.gradeList);
        }

        var response = await controller.UpdateGrade(getTeacherEnrollmentByPartialDto(), gradeList);
        if (response)
        {
            await Notificator.ShowSuccess("Éxito", "Hemos guardado las calificaciones correctamente.");
            return;
        }

        await Notificator.ShowError("Error", "Tuvimos problemas al guardar las calificaciones.");
    }

    private TeacherEnrollmentByPartialDto getTeacherEnrollmentByPartialDto()
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
}