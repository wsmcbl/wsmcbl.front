using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;

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
        return await _apiConsumer.GetPdfAsync(Modules.Academy, resource);
    }
    
    public async Task<List<StudentEntity>?> GetAllStudentsList()
    {
        List<StudentEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, "students", defaultResult);
    }
}