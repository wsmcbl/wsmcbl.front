using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class DebtopController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public DebtopController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<byte[]> GetDebtop()
    {
        var resource = $"documents/debtor";
        return await _apiConsumer.GetPdfAsync(Modules.Accounting, resource);
    }
    
}