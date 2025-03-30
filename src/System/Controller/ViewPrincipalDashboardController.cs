using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.Charts;

namespace wsmcbl.src.Controller;

public class ViewPrincipalDashboardController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public ViewPrincipalDashboardController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<DistributionDto> GetCurrentDistribution()
    {
        DistributionDto defaultResult = new();
        return await _apiConsumer.GetAsync(Modules.Management, "students/distributions", defaultResult);
    }

    public async Task<RevenuesDto> GetCurrentRevenues()
    {
        RevenuesDto defaultResult = new();
        return await _apiConsumer.GetAsync(Modules.Management, "revenues", defaultResult);
    }
}