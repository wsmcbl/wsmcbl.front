using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

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
        var token = await _localStorage.GetAsync<string>(Utilities.TokenKey);
        if (string.IsNullOrEmpty(token.Value))
            return new AuthenticationState(_anonymous);

        var claims = JwtParser.ParseClaimsFromJwt(token.Value);
        var identity = new ClaimsIdentity(claims, "jwtAuth");
        var user = new ClaimsPrincipal(identity);

        Console.WriteLine("Authenticated User Roles:");
        foreach (var claim in claims)
        {
            Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
        }

        return new AuthenticationState(user);
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