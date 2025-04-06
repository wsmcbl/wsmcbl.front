using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config.CreateNewUser;
using wsmcbl.src.View.Config.UserInformationView;
using wsmcbl.src.View.Config.UserList;

namespace wsmcbl.src.Controller;

public class CreateUserController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public CreateUserController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<Paginator<UserToListDto>> GetUserList(PagedRequest pagedRequest)
    {
        Paginator<UserToListDto> defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Config, $"users{pagedRequest}", defaultResult);
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

    public async Task<bool> UpdateUser(EditUserDto userdata, string userId)
    {
        return await _apiConsumer.PutAsync(Modules.Config, $"users/{userId}", userdata);
    }
    
    public async Task<UserEntity> ChangePassword(string userId)
    {
        var defaultResult = new UserEntity();
        return await _apiConsumer.PutWhitData(Modules.Config, $"users/{userId}/passwords", defaultResult);
    }
    
}