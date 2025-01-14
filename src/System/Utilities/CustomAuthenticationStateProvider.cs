using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;

namespace wsmcbl.src.Utilities;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedLocalStorage _localStorage;
    private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Intenta obtener el token desde el almacenamiento local
        var tokenResult = await _localStorage.GetAsync<string>(Utilities.TokenKey);
        var token = tokenResult.Value;

        // Si no existe un token, retorna un estado no autenticado
        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        try
        {
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            // Verifica si el token es válido (formato JWT)
            if (!handler.CanReadToken(token))
            {
                throw new SecurityTokenException("El token no tiene un formato válido.");
            }

            var jwtToken = handler.ReadJwtToken(token);

            // Extrae las reclamaciones del token
            var claims = jwtToken.Claims.Select(c =>
                    c.Type == "role"
                        ? new Claim(ClaimTypes.Role, c.Value) // Mapea el rol
                        : new Claim(c.Type, c.Value) // Mapea otros valores
            ).ToList();

            // Crea una identidad con las reclamaciones extraídas
            var identity = new ClaimsIdentity(claims, "Bearer");
            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }
        catch (SecurityTokenException ex)
        {
            // Log de errores en el token
            Console.WriteLine($"Token inválido: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Otros errores (por ejemplo, almacenamiento local)
            Console.WriteLine($"Error procesando el token: {ex.Message}");
        }

        // Si ocurre algún error, elimina el token y retorna un estado no autenticado
        await _localStorage.DeleteAsync(Utilities.TokenKey);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
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
}