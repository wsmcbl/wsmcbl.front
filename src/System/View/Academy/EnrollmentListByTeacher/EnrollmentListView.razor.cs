using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentListByTeacher;

public partial class EnrollmentListView : BaseView
{ 
    [Inject] private LoginController loginController { get; set; } = null!;
    [Inject] private AddStudentGradeController Controller { get; set; } = null!;
    
    private UserEntity? user { get; set; }
    private string? TeacherId { get; set; } = string.Empty;
    private List<EnrollmentByTeacherDto> EnrollmentList { get; set; } = new();
    
    protected override async Task OnInitializedAsync()
    {
        user = await loginController.getUserById();
        TeacherId = await loginController.getRoleIdFromToken();
        EnrollmentList = await Controller.GetEnrollmentByTeacherId(TeacherId);
    }

    protected override bool IsLoading()
    {
        return user == null;
    }

    private int getEnrollmentCount() => EnrollmentList.Count;
}