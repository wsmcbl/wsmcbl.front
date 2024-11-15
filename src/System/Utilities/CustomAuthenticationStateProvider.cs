using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace wsmcbl.src.Utilities;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ApiConsumer apiConsumer;
    private readonly ProtectedLocalStorage _localStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ApiConsumer apiConsumer, ProtectedLocalStorage localStorage)
    {
        this.apiConsumer = apiConsumer;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetAsync<string>("authToken");
        if (string.IsNullOrEmpty(token.Value))
            return new AuthenticationState(_anonymous);

        apiConsumer.SetToken(token.Value);
        var identity = new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token.Value), "jwtAuth");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        await _localStorage.SetAsync("authToken", token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOut()
    {
        await _localStorage.DeleteAsync("authToken");
        apiConsumer.ResetToken();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}