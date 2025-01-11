using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Config.CreateUsers;

public partial class ListUsers : ComponentBase
{
    [Inject] CreateUserController Controller { get; set; } = null!;
    private List<ListUserDto>? UserList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadUserList();
    }
    
    private bool IsLoad()
    {
        return UserList == null;
    }

    private async Task LoadUserList()
    {
        UserList = await Controller.GetUserList();
    }
}