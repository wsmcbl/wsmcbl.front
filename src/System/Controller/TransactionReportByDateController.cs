using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports;
using wsmcbl.src.View.Accounting.Reports.Revenue;
using wsmcbl.src.View.Accounting.Transactions;

namespace wsmcbl.src.Controller;

public class TransactionReportByDateController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public TransactionReportByDateController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<TransactionsRevenuesDto> GetReport(DateOnly start, DateOnly end, PagedRequest pagedRequest)
    {
        var startDate = getStringFormat(start);
        var endDate = getStringFormat(end);
        
        var resource = $"transactions/revenues?to={endDate}&from={startDate}{pagedRequest.ToRevenueView()}"; 
        var transactionsRevenues = new TransactionsRevenuesDto();
        return await _apiConsumer.GetAsync(Modules.Accounting, resource, transactionsRevenues);
    }

    private static string getStringFormat(DateOnly date) => date.ToString("dd-MM-yyyy");
    

    public async Task<List<TransactionTypeDto>> GetTypeTransactions()
    {
        List<TransactionTypeDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Accounting, "transactions/types", defaultResult);
    }
    
    public async Task<Paginator<TransactionFullDto>> GetTransactions(PagedRequest pagedRequest)
    {
        Paginator<TransactionFullDto> defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Accounting, $"transactions{pagedRequest}", defaultResult);
    }
}