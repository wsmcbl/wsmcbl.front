using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public class TopNavBar_razor : ComponentBase
{
    [Inject] private CustomAuthenticationStateProvider AuthStateProvider { get; set; }
    [Inject] private Navigator Navigator { get; set; }
    
    public async Task LogOut()
    {
        await AuthStateProvider.MarkUserAsLoggedOut();
        Navigator.ToPage("/");
    }
}