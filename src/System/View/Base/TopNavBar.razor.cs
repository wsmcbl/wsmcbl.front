using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public partial class TopNavBar : ComponentBase
{
    [Inject] private Navigator? Navigator { get; set; }
    [Inject] private LoginController controller { get; set; } = null!;
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    private UserEntity? User { get; set; }

    private async Task LogOut()
    {
        await AuthStateProvider!.MarkUserAsLoggedOut();
        await Navigator!.HideModal("logoutModal");
        Navigator.ToPage("/");
    }

    protected override async Task OnInitializedAsync()
    {
        User = await controller.getUserById();
    }
}