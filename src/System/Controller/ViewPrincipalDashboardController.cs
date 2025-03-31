using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.Charts;

namespace wsmcbl.src.Controller;

public class ViewPrincipalDashboardController : BaseController
{
    public ViewPrincipalDashboardController(ApiConsumerFactory apiFactory) : base(apiFactory, string.Empty)
    {
    }
    
    public async Task<DistributionDto> GetCurrentDistribution()
    {
        DistributionDto defaultResult = new();
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, "students/distributions", defaultResult);
    }

    public async Task<RevenuesDto> GetCurrentRevenues()
    {
        RevenuesDto defaultResult = new();
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, "revenues", defaultResult);
    }
}