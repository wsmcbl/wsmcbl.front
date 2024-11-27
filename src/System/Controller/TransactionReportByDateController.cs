using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Accounting.Reports;

namespace wsmcbl.src.Controller;

public class TransactionReportByDateController
{
    private ApiConsumer Consumer;
    public TransactionReportByDateController(ApiConsumer consumer)
    {
        Consumer = consumer;
    }
    
    public async Task<TransactionsRevenuesDto> GetTransactionsRevenues(int type)
    {
        var resource = $"transactions/revenues?q={type}"; 
        TransactionsRevenuesDto? transactionsRevenues = new TransactionsRevenuesDto();
        return await Consumer.GetAsync(Modules.Accounting, resource, transactionsRevenues);
    }

    public async Task<List<TypeTransactionsDto>> GetTypeTransactions()
    {
        List<TypeTransactionsDto> defaultResult = [];
        return await Consumer.GetAsync(Modules.Accounting, "transactions/types", defaultResult);
    }
}