using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Pages.HomePages;

public partial class Teacher : ComponentBase
{
    [Inject] protected JwtClaimsService JwtClaimsService { get; set; } = null!;
    private string? TeacherId { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        TeacherId = await JwtClaimsService.GetClaimAsync("roleid");
    }
}