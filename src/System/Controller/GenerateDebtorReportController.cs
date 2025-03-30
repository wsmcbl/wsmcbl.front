using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class GenerateDebtorReportController : BaseController
{
    public GenerateDebtorReportController(ApiConsumerFactory apiFactory) : base(apiFactory, "debtors/export")
    {
    }
    
    public async Task<byte[]> GetDebtorReport()
    {
        return await apiFactory.WithNotificator.GetPdfAsync(Modules.Accounting, path);
    }
}