using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public PrintReportCardStudentController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<byte[]> GetPdfContent(string studentId)
    {
        var resource = $"documents/report-cards/{studentId}";
        return await _apiConsumer.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task<Paginator<StudentEntity>> GetAllStudentsList(PagedRequest pagedRequest)
    {
        return await _apiConsumer.GetAsync(Modules.Secretary, $"students{pagedRequest}", new Paginator<StudentEntity>());
    }
}