using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Base
{
    public class SidebarNav_razor : ComponentBase
    {
        [Inject] private CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected bool IsAdmin { get; set; } 
        protected bool IsCashier { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;

            if (user != null)
            {
                IsAdmin = user.IsInRole("admin");
               IsCashier = user.IsInRole("cashier");
            }
        }
    }
}