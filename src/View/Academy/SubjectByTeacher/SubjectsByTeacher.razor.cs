using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Academy.EnrollmentListByTeacher;

namespace wsmcbl.src.View.Academy.SubjectByTeacher;

public partial class SubjectsByTeacher : ComponentBase
{
    [Inject] private TeacherDashboardController TeacherDashboardController { get; set; } = null!;
    [Inject] private CreateSubjectDataController CreateSubjectDataController { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;

    [Parameter] public string? TeacherId { get; set; }
    private List<SubjectByTeacherDto> SubjectListForTeacher { get; set; } = [];
    private List<SubjectDataEntity>? Subjects { get; set; }
    private List<SubjectAreaEntity> AreaList { get; set; } = [];
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = [];



    protected override async Task OnParametersSetAsync()
    {
        if (TeacherId != null)
        {
            SubjectListForTeacher = await TeacherDashboardController.GetSubjectTeacherListById(TeacherId);
            Subjects = await CreateSubjectDataController.GetSubjectList();
            AreaList = await CreateSubjectDataController.GetAreaList();
            EnrollmentList = await AddingStudentGradesController.GetEnrollmentList(TeacherId);
        }
    }
}