using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Config.UserInformationView;

public partial class ViewUserInfo : BaseView
{
    [Parameter] public string? userId { get; set; }
    [Inject] private CreateUserController CreateUserController { get; set; } = null!;
    [Inject] private LoginController LoginController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    private List<PermissionsDto> permissions { get; set; } = [];
    private EditUserDto EditUser { get; set; } = new();
    private string RoleName { get; set; } = string.Empty;
    private UserEntity User {get; set;} = null!;

    
    
    protected override async Task OnParametersSetAsync()
    {
        if (userId is not null)
        {
            permissions = await CreateUserController.GetPermissionList();
            User = await LoginController.getUserById(userId);
            GetRoleName();
            ConfigEditUser();
        }
    }
    
    private  void GetRoleName()
    {
        RoleName = Enum.IsDefined(typeof(Role), User.roleId) 
            ? ((Role)User.roleId).ToString() 
            : "Sin asignar";
    }

    private void ConfigEditUser()
    {
        EditUser.name = User.name;
        EditUser.secondName = User.secondName;
        EditUser.surname = User.surName;
        EditUser.secondSurname = User.secondSurname;
        EditUser.permissionList = User.permissionList;
        EditUser.nextCloudGroup = User.nextCloudGroup;
    }
    
    private bool ValidData()
    {
        if (EditUser.name.IsNullOrEmpty() || EditUser.surname.IsNullOrEmpty())    
        {
            return false;
        }
        return true;
    }

    private void AddPermissions(int permission)
    {
        if (EditUser.permissionList.Contains(permission))
        {
            EditUser.permissionList.Remove(permission);
            return;
        }
        EditUser.permissionList.Add(permission);
    }
    
    private async Task UpdateUser()
    {
        if (!ValidData())
        {
            await Notificator.ShowError("Error","Valide los datos ingresados");
        }
        var toBasicUser = EditUser.ToBasicUserInfo();
        var response = await CreateUserController.UpdateUser(toBasicUser, userId!);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado los datos del usuario con exito");
            await OnParametersSetAsync();
            return;
        }
        
        await Notificator.ShowError("Error", "No hemos actualizado los datos");
    }
    
    private async Task UpdateUserPermissions()
    {
        var response = await CreateUserController.UpdateUserPermissions(userId!, EditUser.permissionList);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado los permisos del usuario con exito");
            await OnParametersSetAsync();
            return;
        }
        await Notificator.ShowError("Error", "No hemos actualizado los permisos");
    }
    
    protected override bool IsLoading()
    {
        return User == null;
    }
    
}