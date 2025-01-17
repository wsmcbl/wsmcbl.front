using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Config.Authentication;

public partial class LoginView : ComponentBase
{
    private string? errorMessage { get; set; }
    private string email { get; set; } = null!;
    private string password { get; set; } = null!;
    
    [Inject] private Navigator? Navigator { get; set; }
    [Inject] private LoginController controller { get; set; } = null!;
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }

    private async Task Login()
    {
        errorMessage = null;
        var token = await controller.login(email, password);
        if (token == string.Empty)
        {
            errorMessage = controller.errorMessage;
            return;
        }
        
        await AuthStateProvider!.MarkUserAsAuthenticated(token); 
        StateHasChanged();
        await Task.Delay(100);
        Navigator!.ToPage("/dashboard");
    }
}