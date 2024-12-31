using System.Security.Claims;

namespace wsmcbl.src.Utilities;

public class JwtClaimsService
{
    private readonly CustomAuthenticationStateProvider _authStateProvider;
    
    public JwtClaimsService(CustomAuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }
    
    /// <summary>
    /// Obtiene el valor de un claim específico.
    /// </summary>
    public async Task<string?> GetClaimAsync(string claimType)
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
        {
            var claim = user.FindFirst(claimType);
            return claim?.Value;
        }

        return null;
    }
    
    /// <summary>
    /// Obtiene todos los claims del usuario autenticado.
    /// </summary>
    public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity is { IsAuthenticated: true }
            ? user.Claims
            : Enumerable.Empty<Claim>();
    }

    /// <summary>
    /// Comprueba si el usuario tiene un rol específico.
    /// </summary>
    public async Task<bool> IsInRoleAsync(string role)
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.IsInRole(role);
    }
    
    
}