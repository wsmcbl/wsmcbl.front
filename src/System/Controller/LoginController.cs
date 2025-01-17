using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Config;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config.Authentication;

namespace wsmcbl.src.Controller;

public class LoginController
{
    private readonly ApiConsumer _apiConsumer;
    private readonly JwtClaimsService _jwtClaimsService;

    public LoginController(ApiConsumer apiConsumer, JwtClaimsService jwtClaimsService)
    {
        _apiConsumer = apiConsumer;
        _jwtClaimsService = jwtClaimsService;
    }
    
    public string? errorMessage { get; set; }
    
    public async Task<string> login(string email, string password)
    {
        var defaultDto = new LoginDto();
        defaultDto.SetAsDefault();
        
        var data = new LoginDto(email, password);
        try
        {
            var result = await _apiConsumer.PostAsync(Modules.Config, "users/tokens", data, defaultDto);
            return result.token!;
        }
        catch (Exception e)
        {
            errorMessage = e.Message;
            return string.Empty;
        }
    }

    public async Task<UserEntity> getUserById()
    {
        var userId = await _jwtClaimsService.GetClaimAsync("userid");
        if (userId == null)
        {
            throw new InternalException("User has not userId in jwt.");
        }
        
        return await _apiConsumer.GetAsync(Modules.Config, $"/users/{userId}", new UserEntity());
    }

    public async Task<string> getRoleIdFromToken()
    {
        var id =  await _jwtClaimsService.GetClaimAsync("roleid");
        if (id == null)
        {
            throw new InternalException("User has not roleId in jwt.");
        }

        return id;
    }
}