using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports.Revenue;
using wsmcbl.src.View.Accounting.Transactions;

namespace wsmcbl.src.Controller;

public class CancelTransactionController : BaseController
{
    public CancelTransactionController(ApiConsumerFactory apiFactory) : base(apiFactory, "transactions")
    {
    }
    
    public async Task<Paginator<TransactionFullDto>> GetTransactionList(PagedRequest pagedRequest)
    {
        var resource = $"{path}{pagedRequest}";
        return await apiFactory.WithNotificator.GetAsync(Modules.Accounting, resource, new Paginator<TransactionFullDto>());
    }

    public async Task<bool> CancelTransactionById(string transactionId)
    {
        var resource = $"{path}/{transactionId}";
        return await apiFactory.WithNotificator.PutAsync(Modules.Accounting, resource, transactionId);
    }
    
    public async Task<byte[]> GetInvoice(string transactionId)
    {
        var resource = $"{path}/{transactionId}/invoices";
        return await apiFactory.WithNotificator.GetPdfAsync(Modules.Accounting, resource);
    }

    public async Task<List<TransactionTypeDto>?> GetTariffTypeList()
    {
        var controller = new TransactionReportByDateController(apiFactory);
        return await controller.GetTypeTransactions();
    }
}