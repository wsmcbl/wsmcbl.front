using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller.Service;

public class ApiConsumerFactory
{
    private readonly HttpClient _httpClient;
    private readonly Notificator _notificator;
    private readonly ProtectedLocalStorage _localStorage;

    public ApiConsumerFactory(HttpClient httpClient, Notificator notificator, ProtectedLocalStorage localStorage)
    {
        _httpClient = httpClient;
        _notificator = notificator;
        _localStorage = localStorage;
    }

    private ApiConsumer? _default;
    public ApiConsumer Default => _default ??= new ApiConsumer(_httpClient, _localStorage);
    
    private ApiConsumerWithNotificator? _withNotificator;
    public ApiConsumerWithNotificator WithNotificator => _withNotificator ??=
        new ApiConsumerWithNotificator(_notificator, _httpClient, _localStorage);
}