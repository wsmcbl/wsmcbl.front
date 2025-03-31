using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController : BaseController
{
    public PrintReportCardStudentController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<byte[]> GetPdfContent(string studentId)
    {
        var resource = $"students/{studentId}/report-card/export";
        
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
    }
}