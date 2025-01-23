using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting.Reports;
using wsmcbl.src.View.Accounting.Transactions;

namespace wsmcbl.src.Controller;

public class TransactionReportByDateController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    public TransactionReportByDateController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<TransactionsRevenuesDto> GetReport(DateOnly start, DateOnly end)
    {
        var startDate = getStringFormat(start);
        var endDate = getStringFormat(end);
        
        var resource = $"transactions/revenues?start={startDate}&end={endDate}"; 
        var transactionsRevenues = new TransactionsRevenuesDto();
        return await _apiConsumer.GetAsync(Modules.Accounting, resource, transactionsRevenues);
    }

    private static string getStringFormat(DateOnly date) => date.ToString("dd-MM-yyyy");
    

    public async Task<List<TransactionTypeDto>> GetTypeTransactions()
    {
        List<TransactionTypeDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Accounting, "transactions/types", defaultResult);
    }
    
    public async Task<List<TransactionFullDto>> GetTransactions()
    {
        List<TransactionFullDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Accounting, "transactions", defaultResult);
    }
}