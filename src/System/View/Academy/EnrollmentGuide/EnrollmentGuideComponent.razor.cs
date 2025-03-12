using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Academy.EnrollmentGuide;

public partial class EnrollmentGuideComponent : ComponentBase
{
    [Inject] EnrollmentGuideController Controller { get; set; } = default!;
    [Inject] JwtClaimsService JwtClaimsService { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;

    private EnrollmentDto Enrollment { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        var token = await JwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrEmpty(token))
        {
            await Notificator.ShowWarning("Advertencia", "Estamos teniendo problemas para obtener tu información," +
                                                         "intentelo mas tarde");
            return;
        }

        Enrollment = await Controller.GetMyEnrollmentGuide(token);
    }
}