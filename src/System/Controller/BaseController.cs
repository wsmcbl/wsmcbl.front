using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class BaseController(ApiConsumerFactory apiFactory, string path)
{
    protected string path { get; } = path;
    protected ApiConsumerFactory apiFactory { get; } = apiFactory;
}