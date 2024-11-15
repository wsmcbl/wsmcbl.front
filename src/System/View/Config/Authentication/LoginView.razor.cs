using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Config.Authentication;

public partial class LoginView : ComponentBase
{
    private LoginDto User = new();
    [Inject] private ApiConsumer Consumer { get; set; }
    [Inject] private Navigator Navigator { get; set; }
    [Inject] private Notificator Notificator { get; set; }
    [Inject] private CustomAuthenticationStateProvider AuthStateProvider { get; set; }

    public async Task Login()
    {
        var token = await Consumer.LoginAsync(User);
        
        if (token == string.Empty)
        {
            Notificator.ShowError("Error al iniciar sesión.","Revise el correo y contraseña ingresada.");
            return;
        }
        
        await AuthStateProvider.MarkUserAsAuthenticated(token);
        Navigator.ToPage("/");
    }
}