using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Pages.HomePages;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentGuide;

public partial class EnrollmentGuideComponent : BaseView
{
    [Inject] private ViewEnrollmentGuideController GuideController { get; set; } = null!;
    [Inject] private UpdateOfficialEnrollmentController EnrollmentController { get; set; } = null!;
    [Inject] private LoginController LoginController { get; set; } = null!;
    [Inject] private JwtClaimsService JwtClaimsService { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;

    private EnrollmentDto Enrollment { get; set; } = new();
    private List<TeacherEntity> Teachers { get; set; } = new();
    private UserEntity User { get; set; } = new();
    private string? TeacherId { get; set; }
    

    protected override async Task OnInitializedAsync()
    {
        var token = await JwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrEmpty(token))
        {
            await Notificator.ShowWarning("Advertencia", "Estamos teniendo problemas para obtener tu información," +
                                                         "intentelo mas tarde");
            return;
        }

        TeacherId = token;
        Enrollment = await GuideController.GetMyEnrollmentGuide(TeacherId);
        if (Enrollment?.studentList?.Count > 0)
        {
            Enrollment.studentList = Enrollment.studentList.OrderBy(s => s.sex).ThenBy(s => s.fullName).ToList();
        }
        
        Teachers = await EnrollmentController.GetActiveTeacherList();
        User = await LoginController.getUserById();
        
        if (Enrollment?.studentList == null)
        {
            if (Enrollment != null) Enrollment.studentList = new List<StudentDto>();
        }
    }
    
    protected override bool IsLoading()
    {
        if (Enrollment == null || Enrollment.studentList == null || Enrollment.studentList.Count == 0)
        {
            return false; 
        }

        return true; 
    }

    private async Task ViewPerformanceReport()
    {
        await Navigator.ShowModal("ReportGradeModal");
    }
}