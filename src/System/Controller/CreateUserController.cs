using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Config;
using wsmcbl.src.View.Config.CreateNewUser;
using wsmcbl.src.View.Config.UserList;

namespace wsmcbl.src.Controller;

public class CreateUserController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public CreateUserController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<List<UserToListDto>> GetUserList()
    {
        var resource = "users"; 
        var defaultResult = new List<UserToListDto>();
        return await _apiConsumer.GetAsync(Modules.Config, resource, defaultResult);
    }
    
    public async Task<List<PermissionsDto>> GetPermissionList()
    {
        var resource = "permissions"; 
        var defaultResult = new List<PermissionsDto>();
        return await _apiConsumer.GetAsync(Modules.Config, resource, defaultResult);
    }
    
    public async Task<List<string>> GetNextclodGroups()
    {
        var resource = "nextcloud/groups"; 
        var defaultResult = new List<string>();
        return await _apiConsumer.GetAsync(Modules.Config, resource, defaultResult);
    }

    public async Task<UserEntity> CreateNewUser(CreateUserDto userdata)
    {
        var resource = "users";
        var defaultResult = new UserEntity();
        return await _apiConsumer.PostAsync(Modules.Config, resource, userdata, defaultResult);
    }
    
    
}