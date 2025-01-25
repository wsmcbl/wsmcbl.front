using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;

namespace wsmcbl.src.Utilities;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;

    public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var browserStorage = await _localStorage.GetAsync<string>(Utilities.TokenKey);
            return await BuildAuthentication(browserStorage.Value!);
        }
        catch (Exception)
        {
            return await NotAuthenticated();
        }
    }


    private JwtSecurityTokenHandler handler { get; set; } = null!;
    private async Task<AuthenticationState> BuildAuthentication(string token)
    {
        try
        {
            handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token) || IsTokenExpired(token))
            {
                throw new SecurityTokenException("El token no tiene un formato válido.");
            }

            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims.Select(c =>
                c.Type == "role"
                    ? new Claim(ClaimTypes.Role, c.Value)
                    : new Claim(c.Type, c.Value)).ToList();

            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }
        catch (SecurityTokenException ex)
        {
            Console.WriteLine($"Token inválido: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error procesando el token: {ex.Message}");
        }

        return await NotAuthenticated();
    }

    private bool IsTokenExpired(string token)
    {
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.ValidTo < DateTime.UtcNow;
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await _localStorage.SetAsync(Utilities.TokenKey, token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.DeleteAsync(Utilities.TokenKey);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private async Task<AuthenticationState> NotAuthenticated()
    {
        await _localStorage.DeleteAsync(Utilities.TokenKey);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }
}