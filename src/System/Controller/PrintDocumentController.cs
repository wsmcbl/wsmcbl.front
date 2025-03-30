using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class PrintDocumentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public PrintDocumentController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }

    public async Task<byte[]> GetOfficialEnrollmentListDocument()
    {
        return await _apiConsumer.GetByteFileAsync(Modules.Secretary, "degrees/documents");
    }
}