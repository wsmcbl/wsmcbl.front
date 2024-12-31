using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.ListOfEnrollmentByTeacher;

public partial class ListOfEnrollments : ComponentBase
{ 
    [Inject] private ApiConsumer? Consumer { get; set; }
    [Inject] private AddStudentGradeController Controller { get; set; } = null!;
    [Inject] private JwtClaimsService ClaimsService { get; set; } = null!;
    private UserDto? User { get; set; }
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = new();
    private string? TeacherId = string.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        User = await GetUser();
        TeacherId = await ClaimsService.GetClaimAsync("roleid");
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