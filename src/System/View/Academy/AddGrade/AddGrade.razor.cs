using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Components.MoveTeacherGuide;

namespace wsmcbl.src.View.Academy.AddGrade;

public partial class AddGrade : ComponentBase
{
   
    [Inject] private AddStudentGradeController GradeController { get; set; } = null!;
    [Inject] private MoveTeacherGuideFromEnrollmentController TeacherController { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string TeacherId { get; set; } = null!;
    [Parameter, SupplyParameterFromQuery] public string? DegreeName { get; set; }
    [Parameter] public string EnrollmentId { get; set; } = null!;
    
    private List<PartialsListDto> partialsList = [];
    private List<TeacherNoGuideDto> TeacherListAvailable = [];
    private GetFullInformationDto GetFullInformation { get; set; } = new();
    private FullInformationofEnrollmentDto FullInformationOfEnrollment { get; set; } = null!;
    private List<StudentSubjectGradeDto> StudentData { get; set; } = new();
    
    private string? TeacherName = string.Empty;
    private int Counter;
    private int Counter2;


    protected override async Task OnParametersSetAsync()
    {
        await GetPartials();
        await GetTeachersAvailable();
        await GetNameOfTeacher();
        await GetFullInfoEnrollment();
    }

    private async Task GetPartials()
    {
        partialsList = await GradeController.GetPartialsList();
    }
    
    private async Task GetTeachersAvailable()
    {
        TeacherListAvailable = await TeacherController.GetActiveTeachers();
    }
    
    private Task GetNameOfTeacher()
    {
        TeacherName = TeacherListAvailable.FirstOrDefault(t => t.teacherId == TeacherId)?.fullName;
        return Task.CompletedTask;
    }
    
    private async Task GetFullInfoEnrollment()
    {
        GetFullInformation.teacherId = TeacherId;
        GetFullInformation.enrollmentId = EnrollmentId;
        GetFullInformation.partialId = 1;
        
        FullInformationOfEnrollment = await GradeController.GetFullInformationOfEnrollment(GetFullInformation);
        StudentData = MakeData(FullInformationOfEnrollment);
    }

    private List<StudentSubjectGradeDto> MakeData(FullInformationofEnrollmentDto data)
    {
        var result = new List<StudentSubjectGradeDto>();
        
        foreach (var student in data.studentList)
        {
            var studentSubjectGrade = new StudentSubjectGradeDto()
            {
                StudentId = student.studentId,
                StudentName = student.fullName
            };

            foreach (var subject in data.subjectList)
            {
                var grades = data.subjectPartialList
                    .Where(p => p.subjectId == subject.subjectId)
                    .SelectMany(p => p.Grades)
                    .ToList();

                studentSubjectGrade.SubjectGrades[subject.name] = grades;
            }

            result.Add(studentSubjectGrade);
        }

        return result;
    }
    
    
    
}