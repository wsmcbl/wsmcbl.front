using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Config.UserInformationView;

public partial class ViewUserInfo : BaseView
{
    [Parameter] public string userId { get; set; } = null!;
    [Inject] private CreateUserController CreateUserController { get; set; } = null!;
    [Inject] private LoginController LoginController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    private List<PermissionsDto> permissions { get; set; } = [];
    private EditUserDto EditUser { get; set; } = new();
    private string RoleName { get; set; } = string.Empty;
    private UserEntity User {get; set;} = null!;

    
    
    protected override async Task OnParametersSetAsync()
    {
        permissions = await CreateUserController.GetPermissionList();
        User = await LoginController.getUserById(userId);
        GetRoleName();
        CofigEditUser();
    }
    
    private  void GetRoleName()
    {
        RoleName = Enum.IsDefined(typeof(Role), User!.roleId) 
            ? ((Role)User.roleId).ToString() 
            : "Sin asignar";
    }

    private void CofigEditUser()
    {
        EditUser.name = User.name;
        EditUser.secondName = User.secondName;
        EditUser.surname = User.surName;
        EditUser.secondSurname = User.secondSurname;
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
        
        var response = await CreateUserController.UpdateUser(EditUser!, userId);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado los datos del usuario con exito");
            await OnParametersSetAsync();
            return;
        }
        
        await Notificator.ShowError("Error", "No hemos actualizado los datos");
    }
    
    protected override bool IsLoading()
    {
        return User == null;
    }
    
}