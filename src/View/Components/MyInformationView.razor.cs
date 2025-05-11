using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Config;
using wsmcbl.src.View.Config.UserInformationView;

namespace wsmcbl.src.View.Components;

public partial class MyInformationView : BaseView
{
    [Parameter] public string? userId { get; set; }
    [Inject] private CreateUserController CreateUserController { get; set; } = null!;
    [Inject] private LoginController LoginController { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    private EditUserDto EditUser { get; set; } = new();
    private string RoleName { get; set; } = string.Empty;
    private UserEntity User {get; set;} = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        if (userId is not null)
        {
            User = await LoginController.getUserById(userId);
            GetRoleName();
            CofigEditUser();
        }
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
    
    protected override bool IsLoading()
    {
        return User == null;
    }
    
    
    
}