using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.ListOfEnrollmentByTeacher;

public partial class ListOfEnrollments : ComponentBase
{ 
    [Inject] private ApiConsumer? Consumer { get; set; }
    [Inject] private AddStudentGradeController Controller { get; set; } = null!;
    private UserDto? User { get; set; }
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = new();
    public string TeacherId = "tch-001";
    
    protected override async Task OnInitializedAsync()
    {
        User = await GetUser();
        await GetEnrollments();
    }

    private async Task<UserDto> GetUser()
    {
        return await Consumer!.GetAsync(Modules.Config, "/users", new UserDto());
    }

    private async Task GetEnrollments()
    {
        EnrollmentList = await Controller.GetEnrollmentByTeacherId(TeacherId);
    }
}