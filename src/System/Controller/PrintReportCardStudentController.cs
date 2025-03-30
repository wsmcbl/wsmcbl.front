using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController : BaseController
{
    public PrintReportCardStudentController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<byte[]> GetPdfContent(string studentId)
    {
        var resource = $"students/{studentId}/report-cards/export";
        
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task<Paginator<StudentEntity>> GetAllStudentsList(PagedRequest pagedRequest)
    {
        return await apiFactory.WithNotificator.GetAsync(Modules.Secretary, $"students{pagedRequest}", new Paginator<StudentEntity>());
    }
}