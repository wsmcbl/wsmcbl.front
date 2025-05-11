using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports.Revenue;

namespace wsmcbl.src.Controller;

public class TransactionReportByDateController : BaseController
{
    public TransactionReportByDateController(ApiConsumerFactory apiConsumerFactory) : base(apiConsumerFactory, "transactions")
    {
    }
    
    public async Task<TransactionsRevenuesDto> GetReport(DateOnly start, DateOnly end, PagedRequest pagedRequest)
    {
        var startDate = getStringFormat(start);
        var endDate = getStringFormat(end);
        
        var resource = $"{path}/revenues?to={endDate}&from={startDate}{pagedRequest.ToRevenueView()}"; 
        var transactionsRevenues = new TransactionsRevenuesDto();
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, transactionsRevenues);
    }

    private static string getStringFormat(DateOnly date) => date.ToString("dd-MM-yyyy");

    public async Task<List<TransactionTypeDto>> GetTypeTransactions()
    {
        List<TransactionTypeDto> defaultResult = [];
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, $"{path}/types", defaultResult);
    }
}