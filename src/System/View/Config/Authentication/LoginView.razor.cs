using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Config.Authentication;

public partial class LoginView : ComponentBase
{
    private string? errorMessage { get; set; }
    private string email { get; set; } = null!;
    private string password { get; set; } = null!;
    
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private LoginController controller { get; set; } = null!;
    [Inject] private JwtClaimsService jwtClaimsService { get; set; } = null!;
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var userRole = await jwtClaimsService.GetClaimRole("role");
        var roleRoutes = new Dictionary<string, string>
        {
            { "admin", "/dashboard" },
            { "secretary", "/secretary" },
            { "cashier", "/cashier" },
            { "teacher", "/teacher" },
            { "principal", "/principal" },
            { "viceprincipal", "/viceprincipal" }
        };

        if (userRole is not null && roleRoutes.TryGetValue(userRole, out var route))
        {
            Navigator.ToPage(route);
        }
    }
    private async Task Login()
    {
        errorMessage = "Iniciando sesión ...";
        var token = await controller.login(email, password);
        if (token == string.Empty)
        {
            errorMessage = controller.errorMessage;
            return;
        }
        
        await AuthStateProvider!.MarkUserAsAuthenticated(token); 
        StateHasChanged();
        await Task.Delay(100);
        
        //validar que rol tiene el usuario y redireccionarlo al homepage especifico.
        var userRole = await jwtClaimsService.GetClaimRole("role");
        
        var roleRoutes = new Dictionary<string, string>
        {
            { "admin", "/dashboard" },
            { "secretary", "/secretary" },
            { "cashier", "/cashier" },
            { "teacher", "/teacher" },
            { "principal", "/principal" },
            { "viceprincipal", "/viceprincipal" }
        };

        if (userRole is not null && roleRoutes.TryGetValue(userRole, out var route))
        {
             Navigator!.ToPage(route);
        }
        
    }
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await Login();
        }
    }
}