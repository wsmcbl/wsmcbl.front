using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports.Revenue;

namespace wsmcbl.src.Controller;

public class TransactionReportByDateController : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Notificator _notificator;
    private readonly ApiConsumerFactory _apiConsumerFactory;
    
    public TransactionReportByDateController(ApiConsumerFactory apiConsumerFactory, IJSRuntime jsRuntime, Notificator notificator) : base(apiConsumerFactory, "transactions")
    {
        _jsRuntime = jsRuntime;
        _notificator = notificator;
        _apiConsumerFactory = apiConsumerFactory;
    }
    
    public async Task<TransactionsRevenuesDto> GetReport(DateOnly start, DateOnly end, PagedRequest pagedRequest)
    {
        var startDate = getStringFormat(start);
        var endDate = getStringFormat(end);
        
        var resource = $"{path}/revenues?to={endDate}&from={startDate}{pagedRequest.ToRevenueView()}"; 
        var transactionsRevenues = new TransactionsRevenuesDto();
        return await _apiConsumerFactory.WithNotificator.GetAsync(Modules.Accounting, resource, transactionsRevenues);
    }

    private static string getStringFormat(DateOnly date) => date.ToString("dd-MM-yyyy");

    public async Task<List<TransactionTypeDto>> GetTypeTransactions()
    {
        List<TransactionTypeDto> defaultResult = [];
        return await _apiConsumerFactory.WithNotificator.GetAsync(Modules.Accounting, $"{path}/types", defaultResult);
    }
    
    public async Task GetReportJson(string start, string end)
    {
        var resource = $"{path}/invoices?from={start}&to={end}";
        var fileBytes = await _apiConsumerFactory.WithNotificator.GetByteFileAsync(Modules.Resources, resource);
    
        if (fileBytes == null || fileBytes.Length <= 0)
        {
            await _notificator.ShowError("No se pudo descargar el archivo solicitado.");
            return;
        }
        
        var fileName = $"Transacciones_de_caja_del_{start}_al_{end}.json";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/json;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }
}