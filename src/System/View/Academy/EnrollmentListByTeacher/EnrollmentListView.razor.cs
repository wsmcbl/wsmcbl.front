using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentListByTeacher;

public partial class EnrollmentListView : BaseView
{ 
    [Inject] private LoginController loginController { get; set; } = null!;
    [Inject] private AddStudentGradeController Controller { get; set; } = null!;
    [Inject] private JwtClaimsService ClaimsService { get; set; } = null!;
    private UserDto? User { get; set; }
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = new();
    private string? TeacherId = string.Empty;
    
    protected override async Task OnInitializedAsync()
    {
        var user = await loginController.getUserById();
        TeacherId = await ClaimsService.GetClaimAsync("roleid");
        await GetEnrollments();
    }

    private async Task GetEnrollments()
    {
        EnrollmentList = await Controller.GetEnrollmentByTeacherId(TeacherId);
    }

    protected override bool IsLoading()
    {
        return User == null;
    }
}