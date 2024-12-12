using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller.Service;

public class ApiConsumerWithModal : ApiConsumer
{
    public ApiConsumerWithModal(Notificator notificator, HttpClient httpClient, ProtectedLocalStorage localStorage) :
        base(notificator, httpClient, localStorage)
    {
    }
}