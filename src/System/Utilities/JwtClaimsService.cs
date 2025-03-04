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
    /// Metodo particular para obtener el rol de un user.
    /// </summary>
    public async Task<string?> GetClaimRole(string claimType)
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
        {
            var claim = user.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
            return claim?.Value;
        }

        return null;
    }
    
    /// <summary>
    /// Obtenemos la lista de permisos de usuarios autenticados
    /// </summary>
    public async Task<List<string>> GetUserPermissionsAsync()
    {
        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is not { IsAuthenticated: true })
            return new List<string>();

        var permissionsClaims = user.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();
        return permissionsClaims;
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