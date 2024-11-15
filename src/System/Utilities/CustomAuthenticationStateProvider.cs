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

        var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token.Value), "jwtAuth");
        return new AuthenticationState(new ClaimsPrincipal(identity));
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