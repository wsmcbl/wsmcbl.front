using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Config.UserInformationView;

namespace wsmcbl.src.View.Config.UpdateRole;

public partial class UpdateRoleView : BaseView
{
    [Inject] UpdateRolesController Controller { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;
    private List<RoleListDto> Roles { get; set; } = new();
    private RoleListDto ThisRoles { get; set; } = new();
    private List<PermissionsDto> permissions { get; set; } = [];
    

    protected override async Task OnParametersSetAsync()
    {
        Roles = await Controller.GetRolesList();
        permissions = await Controller.GetPermissionList();
    }
    
    protected override bool IsLoading()
    {
        return Roles.Any();
    }

    private async void OpenRoleDetails(int roleid)
    {
        ThisRoles = await Controller.GetRolesbyId(roleid);
        StateHasChanged();
        await Navigator.ShowModal("UpdateRoleModal");
    }
    
}