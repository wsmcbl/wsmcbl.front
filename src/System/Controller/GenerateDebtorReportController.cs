using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class GenerateDebtorReportController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public GenerateDebtorReportController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<byte[]> GetDebtop()
    {
        var resource = $"documents/debtor";
        return await _apiConsumer.GetPdfAsync(Modules.Accounting, resource);
    }
    
}