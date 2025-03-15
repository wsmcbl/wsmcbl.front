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
    [Inject] private EnrollmentGuideController EnrollmentController { get; set; } = null!;
    [Inject] private UpdateOfficialEnrollmentController Controller { get; set; } = null!;
    [Inject] private LoginController Consumer { get; set; } = null!;
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

        Enrollment = await EnrollmentController.GetMyEnrollmentGuide(token);
        Teachers = await Controller.GetActiveTeacherList();
        User = await Consumer.getUserById();
    }
    
    protected override bool IsLoading()
    {
        return Enrollment.studentList.Count != 0;
    }
}