using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Accounting.Charts;

namespace wsmcbl.src.Controller;

public class DashboardCashierController : BaseController
{
    public DashboardCashierController(ApiConsumerFactory apiFactory) : base(apiFactory, "revenue")
    {
    }
    
    public async Task<SummaryThisMonthDto> GetRevenueMonth(string month)
    {
        var defaultResult = new SummaryThisMonthDto();
        var resource = $"{path}?month={month}";
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);
    }

    public async Task<DetailsThisMonthDto> GetDetails(string month)
    {
        var defaultResult = new DetailsThisMonthDto();
        var resource = $"{path}/expected-monthly?month={month}";
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);    
    }

    public async Task<DistributionLevelsDto> GetDistibution(string month)
    {
        var defaultResult = new DistributionLevelsDto();
        var resource = $"{path}/expected-monthly/received?month={month}";
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, defaultResult);    
    }
}