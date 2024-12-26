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
    private int ActiveTabId = 1;
    private string? TeacherName = string.Empty;


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
        StudentData = MapToStudentSubjectGradeDto(FullInformationOfEnrollment);
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
    
    // Actualización inversa desde StudentSubjectGradeDto a FullInformationofEnrollmentDto
    public void UpdateFullInformationofEnrollmentDto(
        FullInformationofEnrollmentDto enrollmentDto,
        List<StudentSubjectGradeDto> updatedGrades)
    {
        foreach (var studentGrade in updatedGrades)
        {
            foreach (var subjectGrade in studentGrade.SubjectGrades)
            {
                var subjectPartial = enrollmentDto.subjectPartialList
                    .FirstOrDefault(sp => sp.subjectId == subjectGrade.Key);

                if (subjectPartial != null)
                {
                    var gradeEntry = subjectPartial.grades?.FirstOrDefault(g => g.studentId == studentGrade.StudentId);
                    if (gradeEntry != null)
                    {
                        gradeEntry.grade = subjectGrade.Value.Grade;
                        gradeEntry.conductGrade = subjectGrade.Value.ConductGrade;
                        gradeEntry.label = subjectGrade.Value.Label;
                    }
                }
            }
        }
    }


    protected void UpdateConductGrade()
    {
        
    }

    protected void ShowQualification()
    {
        UpdateFullInformationofEnrollmentDto(FullInformationOfEnrollment, StudentData);
        
        foreach (var item in FullInformationOfEnrollment.subjectPartialList)
        {
            foreach (var grade in item.grades!)
            {
                Console.WriteLine("Calificacion id: " + grade.gradeId);
                Console.WriteLine("Asignatura: " + grade.label);
                Console.WriteLine("Student Id: " + grade.studentId);
                Console.WriteLine("Calification: " + grade.grade);
                Console.WriteLine("Conduct: " + grade.conductGrade);
                
            }
        }
    }
    
}