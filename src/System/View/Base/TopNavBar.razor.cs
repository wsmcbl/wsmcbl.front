using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public class TopNavBar_razor : ComponentBase
{
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    [Inject] private Navigator? Navigator { get; set; }
    [Inject] private ApiConsumer? Consumer { get; set; }
    protected UserDto? User { get; set; }
    public async Task LogOut()
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
        return await Consumer!.GetAsync(Modules.Config, "/users", new UserDto());
    }

    
    
}