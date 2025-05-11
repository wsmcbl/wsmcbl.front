using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Config.UpdateRole;
using wsmcbl.src.View.Config.UserInformationView;

namespace wsmcbl.src.Controller;

public class UpdateRolesController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public UpdateRolesController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<List<RoleListDto>> GetRolesList()
    {
        List<RoleListDto> defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Config, "roles", defaultResult);
    }
    
    public async Task<RoleListDto> GetRolesbyId(int roleid)
    {
        RoleListDto defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Config, $"roles/{roleid}", defaultResult);
    }
    
    public async Task<List<PermissionsDto>> GetPermissionList()
    {
        var resource = "permissions"; 
        var defaultResult = new List<PermissionsDto>();
        return await _apiConsumer.GetAsync(Modules.Config, resource, defaultResult);
    }

    public async Task<bool> UpdateRole(RoleListDto role)
    {
        return await _apiConsumer.PutAsync(Modules.Config, $"roles/{role.roleId}", role);
    }
}