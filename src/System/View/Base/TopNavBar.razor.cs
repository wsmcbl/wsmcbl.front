using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public partial class TopNavBar : ComponentBase
{
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    [Inject] private JwtClaimsService JwtClaimsService { get; set; } = null!;
    [Inject] private Navigator? Navigator { get; set; }
    [Inject] private ApiConsumer? Consumer { get; set; }
    protected UserDto? User { get; set; }

    private async Task LogOut()
    {
        await AuthStateProvider!.MarkUserAsLoggedOut();
        await Navigator!.HideModal("logoutModal");
        Navigator.ToPage("/");
    }

    protected override async Task OnInitializedAsync()
    {
        User = await GetUser();
    }

    private async Task<UserDto> GetUser()
    {
        var userId = await JwtClaimsService.GetClaimAsync("nameid");
        return await Consumer!.GetAsync(Modules.Config, $"/users/{userId}", new UserDto());
    }

    
    
}