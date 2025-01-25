using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class PrintDocumentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public PrintDocumentController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }

    public async Task<byte[]> GetAssistanceDocument()
    {
        return await _apiConsumer.GetPdfAsync(Modules.Secretary, "degrees/documents");
    }
}