using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController : BaseController
{
    public PrintReportCardStudentController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<byte[]> GetDegreeWhitToken(string studentId, string token)
    {
        var resource = $"students/{studentId}/report-card/export?adminToken={token}";
        
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task<byte[]> GetDegree(string studentId)
    {
        var resource = $"students/{studentId}/report-card/export";
        return await apiFactory.Default.GetByteFileAsync(Modules.Academy, resource);
    }
}