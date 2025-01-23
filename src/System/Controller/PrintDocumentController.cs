using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class PrintDocumentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public PrintDocumentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }

    public async Task<byte[]> GetAssistanceDocument()
    {
        return await _apiConsumer.GetPdfAsync(Modules.Secretary, "degrees/documents");
    }
}