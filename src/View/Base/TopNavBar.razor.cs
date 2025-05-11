using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public partial class TopNavBar : ComponentBase
{
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    [Inject] private JwtClaimsService jwtClaimsService { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private LoginController Consumer { get; set; } = null!;
    private string? UserId {get; set;}
    private UserEntity? User { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        User = await GetUser();
        
        var userId = await jwtClaimsService.GetClaimAsync("userid");
        if (userId == null)
        {
            throw new InternalException("User has not userId in jwt.");
        }
        UserId = userId;
    }

    private async Task<UserEntity> GetUser()
    {
        return await Consumer.getUserById();
    }
    
    private async Task OpenUserData()
    {
        await Navigator.ShowModal("MyInformationModal");
    }

    private async Task LogOut()
    {
        await AuthStateProvider!.MarkUserAsLoggedOut();
        await Navigator!.HideModal("logoutModal");
        Navigator.ToPage("/");
    }
    
    
}