using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.CreateNewUser;

public partial class CreateNewUserModal : ComponentBase
{
    [Inject] CreateUserController Controller { get; set; } = default!;
    private CreateUserDto User { get; set; } = new();
    private List<PermissionsDto> PermissionsList { get; set; } = [];

    protected override async Task OnParametersSetAsync()
    {
        PermissionsList = await Controller.GetPermissionList();
    }
    
    private void SaveUser()
    {
        Console.WriteLine("permission");
        foreach (var permission in User.permissionList)
        {
            Console.WriteLine(permission);
        }
        Console.WriteLine("permission");
    }
    
    private void PermissionIsSelect(int permissionId)
    {
        if (!User.permissionList.Remove(permissionId))
        {
            User.permissionList.Add(permissionId);
        }
    }
    
    
    
    
    
}