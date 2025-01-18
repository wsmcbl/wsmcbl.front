using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Config.UserList;

public partial class UserListView : BaseView
{
    [Inject] private CreateUserController Controller { get; set; } = null!;
    
    private UserEntity? User { get; set; }
    private List<UserToListDto>? UserList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadUserList();
    }

    protected override bool IsLoading()
    {
        return UserList == null;
    }

    private async Task LoadUserList()
    {
        UserList = await Controller.GetUserList();
    }
    
    private async Task HandleUserUpdated(UserEntity updatedUser)
    {
        User = updatedUser;
        await LoadUserList();
    }
    
}