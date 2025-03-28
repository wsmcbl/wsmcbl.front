using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.Charts;

namespace wsmcbl.src.Controller;

public class DirectorDashboardController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public DirectorDashboardController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<DistributionDto> GetCurrentDistribution()
    {
        DistributionDto defaultResult = new();
        return await _apiConsumer.GetAsync(Modules.Management, "students/distributions", defaultResult);
    }

    public async Task<RevenuesDto> GetCurrenRevenues()
    {
        RevenuesDto defaultResult = new();
        return await _apiConsumer.GetAsync(Modules.Management, "revenues", defaultResult);
    }
}