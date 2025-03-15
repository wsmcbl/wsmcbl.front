using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Pages.HomePages;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.EnrollmentGuide;

public partial class EnrollmentGuideComponent : BaseView
{
    [Inject] private EnrollmentGuideController EnrollmentController { get; set; } = default!;
    [Inject] private UpdateOfficialEnrollmentController Controller { get; set; } = default!;
    [Inject] private JwtClaimsService JwtClaimsService { get; set; } = default!;
    [Inject] private Notificator Notificator { get; set; } = default!;

    private EnrollmentDto Enrollment { get; set; } = new();
    private List<TeacherEntity> Teachers { get; set; } = new();

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
    }
    
    protected override bool IsLoading()
    {
        return Enrollment.studentList.Count != 0;
    }
}