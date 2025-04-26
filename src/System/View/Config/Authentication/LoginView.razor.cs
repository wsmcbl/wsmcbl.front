using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
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
    [Inject] private TurnstileService turnstileService { get; set; } = null!;
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
        if (string.IsNullOrWhiteSpace(_turnstileToken))
        {
            errorMessage = "Complete el captcha";
            return;
        }
        if (!ValidateEmail())
        {
            return;
        }
        var isValid = await turnstileService.ValidateTokenAsync(_turnstileToken);
        if (!isValid)
        {
            errorMessage = "Captcha inválido";
            return;
        }
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
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            await Login();
        }
    }
    private bool ValidateEmail()
    {
        var regex = new Regex(@"^[a-zA-Z]+\.[a-zA-Z]+(\d{2})?@cbl-edu\.com$", RegexOptions.IgnoreCase);
        if (string.IsNullOrWhiteSpace(email))
        {
            errorMessage = "El correo electrónico es requerido";
            return false;
        }
        if (!regex.IsMatch(email))
        {
            errorMessage = "Formato inválido. Use: su correo institucional";;
            return false;
        }
        if (email.Any(char.IsDigit))
        {
            var digitCount = email.Split('@')[0].ToCharArray().Count(char.IsDigit);
            if (digitCount != 2)
            {
                errorMessage = "Debe incluir exactamente 2 dígitos cuando use números";
                return false;
            }
        }
        errorMessage = "";
        return true;
    }
    //captcha
    private string? _turnstileToken;
    private void TurnstileCallback(string token)
    {
        _turnstileToken = token;
        StateHasChanged();
    }

    private void TurnstileErrorCallback()
    {
        Console.WriteLine("Error al cargar Turnstile.");
    }
}