using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config.Authentication;

namespace wsmcbl.src.Controller;

public class LoginController
{
    private ApiConsumerWithNotificator _apiConsumer;
    private JwtClaimsService _jwtClaimsService;

    public LoginController(ApiConsumerWithNotificator apiConsumer, JwtClaimsService jwtClaimsService)
    {
        _apiConsumer = apiConsumer;
        _jwtClaimsService = jwtClaimsService;
    }
    
    public async Task<string> login(string email, string password)
    {
        var defaultDto = new LoginDto();
        defaultDto.SetAsDefault();
        
        var data = new LoginDto(email, password);
        var result = await _apiConsumer.PostAsync(Modules.Config, "users/tokens", data, defaultDto);
        
        return result.token!;
    }
}