using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.CreateNewUser;
using wsmcbl.src.View.Config.CreateUsers;

namespace wsmcbl.src.Controller;

public class CreateUserController
{
    private ApiConsumerWithNotificator _apiConsumer;
    public CreateUserController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<List<ListUserDto>> GetUserList()
    {
        var resource = "users"; 
        var defaultResult = new List<ListUserDto>();
        return await _apiConsumer.GetAsync(Modules.Management, resource, defaultResult);
    }
    
    public async Task<List<PermissionsDto>> GetPermissionList()
    {
        var resource = "permissions"; 
        var defaultResult = new List<PermissionsDto>();
        return await _apiConsumer.GetAsync(Modules.Management, resource, defaultResult);
    }
    
}