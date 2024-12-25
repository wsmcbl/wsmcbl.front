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
    }
}