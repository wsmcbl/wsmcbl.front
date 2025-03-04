using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public partial class SidebarCashier : ComponentBase
{
    [Inject] private Navigator? Navigator { get; set; }
    [Inject] private LoginController controller { get; set; } = null!;
    [Inject] private JwtClaimsService jwtClaimsService { get; set; } = null!;
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    private UserEntity? User { get; set; }
    protected string? UserRole { get; set; }
    private List<string> UserPermissions { get; set; } = new();
    
    private async Task LogOut()
    {
        await AuthStateProvider!.MarkUserAsLoggedOut();
        await Navigator!.HideModal("logoutModal");
        Navigator.ToPage("/");
    }
    
    protected override async Task OnInitializedAsync()
    {
        UserRole = await jwtClaimsService.GetClaimRole("role");
        UserPermissions = await jwtClaimsService.GetUserPermissionsAsync();
    }

    private bool HasPermission(string permission)
    {
        return UserPermissions.Contains(permission);
    }

    private bool HasAnyPermission(params string[] permissions)
    {
        return permissions.Any(p => UserPermissions.Contains(p));
    }
    
}