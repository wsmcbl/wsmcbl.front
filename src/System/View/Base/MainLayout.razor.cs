using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base;

public partial class MainLayout
{
    [Inject] private CustomAuthenticationStateProvider? AuthStateProvider { get; set; }
    private AuthenticationState? authState;
    private bool IsLoading { get; set; } = true;
    protected bool IsAuthenticated { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            authState = await AuthStateProvider?.GetAuthenticationStateAsync()!;
            if (authState?.User.Identity is not null)
            {
                IsAuthenticated = authState.User.Identity.IsAuthenticated;
            }
        }
        catch
        {
            IsAuthenticated = false;
        }
        finally
        {
            IsLoading = false;
        }
    }
}