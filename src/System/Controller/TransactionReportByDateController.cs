using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting.Reports;
using wsmcbl.src.View.Accounting.Transactions;

namespace wsmcbl.src.Controller;

public class TransactionReportByDateController
{
    private ApiConsumerWithNotificator _apiConsumer;
    public TransactionReportByDateController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<TransactionsRevenuesDto> GetReport(int type)
    {
        var resource = $"transactions/revenues?q={type}"; 
        var transactionsRevenues = new TransactionsRevenuesDto();
        return await _apiConsumer.GetAsync(Modules.Accounting, resource, transactionsRevenues);
    }

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