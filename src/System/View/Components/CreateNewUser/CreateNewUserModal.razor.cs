using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.CreateNewUser;

public partial class CreateNewUserModal : ComponentBase
{
    [Parameter] public UserEntity? UserEntity { get; set; }
    [Parameter] public EventCallback<UserEntity> OnUserUpdated { get; set; }
    [Inject] CreateUserController Controller { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;
    private CreateUserDto User { get; set; } = new();
    private List<PermissionsDto> PermissionsList { get; set; } = [];
    private List<string> NextcloudGroup { get; set; } = [];
    private bool isLoading = false;

    protected override async Task OnParametersSetAsync()
    {
        PermissionsList = await Controller.GetPermissionList();
        NextcloudGroup = await Controller.GetNextclodGroups();
    }
    
    private void PermissionIsSelect(int permissionId)
    {
        if (!User.permissionList.Remove(permissionId))
        {
            User.permissionList.Add(permissionId);
        }
    }
    
    private async Task SaveUser()
    {
        if (!User.IsDataValid())
        {
            await Notificator.ShowInformation("Advertencia", "Todos los campos con * son obligatorios");
            return;
        }
        
        isLoading = true;
        UserEntity = await Controller.CreateNewUser(User);
        if (UserEntity != null)
        {
            await OnUserUpdated.InvokeAsync(UserEntity);
            isLoading = false;
            await Navigator.HideModal("NewUserModal");
            await Navigator.ShowModal("InfoUserModal");
            return;
        }
        await Notificator.ShowInformation("Error", "No hemos podido crear el usuario intentelo mas tarde");
        isLoading = false;
    }
    
    
    
    
    
}