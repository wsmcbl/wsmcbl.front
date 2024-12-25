using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy;

public partial class ListOfEnrollments : ComponentBase
{ 
    [Inject] private ApiConsumer? Consumer { get; set; }
    protected UserDto? User { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        User = await GetUser();
    }

    private async Task<UserDto> GetUser()
    {
        return await Consumer!.GetAsync(Modules.Config, "/users", new UserDto());
    }
}