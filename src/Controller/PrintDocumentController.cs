using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class PrintDocumentController : BaseController
{
    public PrintDocumentController(ApiConsumerFactory apiFactory) : base(apiFactory, string.Empty)
    {
    }

    public async Task<byte[]> GetOfficialEnrollmentListDocument()
    {
        return await apiFactory
            .WithNotificator.GetByteFileAsync(Modules.Secretary, "degrees/export");
    }
}