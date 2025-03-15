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
    [Inject] private EnrollmentGuideController GuideController { get; set; } = null!;
    [Inject] private UpdateOfficialEnrollmentController EnrollmentController { get; set; } = null!;
    [Inject] private LoginController LoginController { get; set; } = null!;
    [Inject] private JwtClaimsService JwtClaimsService { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;

    private EnrollmentDto Enrollment { get; set; } = new();
    private List<TeacherEntity> Teachers { get; set; } = new();
    private UserEntity User { get; set; } = new();
    

    protected override async Task OnInitializedAsync()
    {
        var token = await JwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrEmpty(token))
        {
            await Notificator.ShowWarning("Advertencia", "Estamos teniendo problemas para obtener tu información," +
                                                         "intentelo mas tarde");
            return;
        }

        Enrollment = await GuideController.GetMyEnrollmentGuide(token);
        Teachers = await EnrollmentController.GetActiveTeacherList();
        User = await LoginController.getUserById();
    }
    
    protected override bool IsLoading()
    {
        return Enrollment.studentList.Count != 0;
    }
}