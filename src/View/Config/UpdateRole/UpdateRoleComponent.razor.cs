using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Config.UserInformationView;

namespace wsmcbl.src.View.Config.UpdateRole;

public partial class UpdateRoleComponent : BaseView
{
    [Inject] UpdateRolesController Controller { get; set; } = null!;
    [Inject] Notificator Notificator { get; set; } = null!;
    [Parameter] public List<PermissionsDto> permissions { get; set; } = new();
    [Parameter] public RoleListDto Role { get; set; } = new();
    
    
    private void AddPermissions(int permission)
    {
        if (Role.permissionList.Contains(permission))
        {
            Role.permissionList.Remove(permission);
            return;
        }
        Role.permissionList.Add(permission);
    }

    private async Task UpdateUser()
    {
        var response = await Controller.UpdateRole(Role);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", $"Hemos actualizado los permisos del rol {Role.name}");
            return;
        }
        await Notificator.ShowError("Error", "Hemos tenido problemas al actualizar los permisos del rol");
    }
}