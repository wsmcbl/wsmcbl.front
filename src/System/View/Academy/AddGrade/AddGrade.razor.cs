using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.AddGrade;

public partial class AddGrade : ComponentBase
{
    [Inject] private AddStudentGradeController GradeController { get; set; } = null!;
    [Inject] private MoveTeacherGuideFromEnrollmentController TeacherController { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string TeacherId { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string? DegreeName { get; set; }
    [Parameter] public string EnrollmentId { get; set; } = null!;
    
    private List<PartialEntity> partialsList = [];
    private List<TeacherEntity> TeacherListAvailable = [];

    private List<SubjectEntity>? subjectList { get; set; }
    private List<StudentEntity>? studentList { get; set; } = [];
    
    private int currentPartial = 0;
    private int ActiveTabId = 1;
    private string? TeacherName = string.Empty;


    protected override async Task OnParametersSetAsync()
    {
        await GetPartials();
        await GetTeachersAvailable();
        GetNameOfTeacher();
        
        var activePartial = partialsList.FirstOrDefault(t => t.isActive);
        currentPartial = activePartial?.partialId ?? 1;
        
        await GetFullInfoEnrollment();
        ActiveTabId = currentPartial;
    }

    private async Task GetPartials()
    {
        partialsList = await GradeController.GetPartialsList();
    }
    
    private async Task GetTeachersAvailable()
    {
        TeacherListAvailable = await TeacherController.GetActiveTeachers();
    }
    
    private void GetNameOfTeacher()
    {
        TeacherName = TeacherListAvailable.FirstOrDefault(t => t.teacherId == TeacherId)?.fullName;
    }
    
    private async Task GetFullInfoEnrollment()
    {
        var dto = new TeacherEnrollmentByPartialDto
        {
            teacherId = TeacherId,
            enrollmentId = EnrollmentId,
            partialId = currentPartial
        };
        
        var result = await GradeController.GetFullInformationOfEnrollment(dto);
        subjectList = result.subjectList;
        studentList = result.studentList;
    }

    private void Testing()
    {
        foreach (var student in studentList!)
        {
            foreach (var grade in student.gradeList!)
            {
                Console.WriteLine(grade.studentId);
                Console.WriteLine(grade.Grade);
            }
        }
    }
    
}