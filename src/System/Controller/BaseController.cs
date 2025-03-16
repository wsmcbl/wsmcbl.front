using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class BaseController(ApiConsumerFactory apiFactory, string resource)
{
    protected string baseModule = resource;
    protected readonly ApiConsumerFactory apiFactory = apiFactory;
}