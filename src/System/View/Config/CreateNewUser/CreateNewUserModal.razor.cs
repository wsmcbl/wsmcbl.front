using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Config.CreateNewUser;

public partial class CreateNewUserModal : ComponentBase
{
    [Parameter] public UserEntity? UserEntity { get; set; }
    [Parameter] public EventCallback<UserEntity> OnUserUpdated { get; set; }
    [Inject] CreateUserController Controller { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;
    private CreateUserDto User { get; set; } = new();
    private List<string> NextcloudGroup { get; set; } = [];
    private bool isLoading = false;

    protected override async Task OnParametersSetAsync()
    {
        NextcloudGroup = await Controller.GetNextclodGroups();
    }
    
    private async Task SaveUser()
    {
        if (!User.IsDataValid())
        {
            await Notificator.ShowInformation("Todos los campos con * son obligatorios.");
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
        await Notificator.ShowError("Hubo un fallo al crear el usuario, inténtelo más tarde.");
        isLoading = false;
    }
    
    
    
    
    
}