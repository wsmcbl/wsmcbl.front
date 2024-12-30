using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Components.MoveTeacherGuide;

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
    
    public List<StudentDto> studentList { get; set; } = null!;
    public List<SubjectsDto> subjectList { get; set; } = null!;
    public List<GradesOfEnrollmentsDto> subjectPartialList { get; set; } = null!;
    private List<StudentSubjectGradeDto> StudentData { get; set; } = new();
    private int currentPartial = 0;
    private int ActiveTabId = 0;
    private string? TeacherName = string.Empty;


    protected override async Task OnParametersSetAsync()
    {
        await GetPartials();
        await GetTeachersAvailable();
        await GetNameOfTeacher();
        await GetFullInfoEnrollment();
        currentPartial = partialsList.First(t => t.isActive).partialId;
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
    
    private Task GetNameOfTeacher()
    {
        TeacherName = TeacherListAvailable.FirstOrDefault(t => t.teacherId == TeacherId)?.fullName;
        return Task.CompletedTask;
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
        StudentData = MapToStudentSubjectGradeDto();
    }
    
    // Conversión desde FullInformationofEnrollmentDto
    private List<StudentSubjectGradeDto> MapToStudentSubjectGradeDto(FullInformationofEnrollmentDto enrollmentDto)
    {
        var result = new List<StudentSubjectGradeDto>();

        foreach (var student in enrollmentDto.studentList)
        {
            var studentSubjectGrades = new StudentSubjectGradeDto
            {
                StudentId = student.studentId,
                FullName = student.fullName
            };

            foreach (var subjectPartial in enrollmentDto.subjectPartialList)
            {
                var gradeEntry = subjectPartial.grades?.FirstOrDefault(g => g.studentId == student.studentId);
                if (gradeEntry != null)
                {
                    studentSubjectGrades.SubjectGrades[subjectPartial.subjectId] = new StudentSubjectGradeDto.GradeInfo
                    {
                        Grade = gradeEntry.grade,
                        ConductGrade = gradeEntry.conductGrade,
                        Label = gradeEntry.label
                    };
                }
            }

            result.Add(studentSubjectGrades);
        }

        return result;
    }
}