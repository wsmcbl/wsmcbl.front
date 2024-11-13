using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController
{
    private ApiConsumer Consumer;
    public PrintReportCardStudentController(ApiConsumer consumer)
    {
        Consumer = consumer;
    }
    public async Task<byte[]> GetPdfContent(string studentId)
    {
        var resource = $"documents/report-cards/{studentId}";
        return await Consumer.GetPdfAsync(Modules.Academy, resource);
    }
    
    public async Task<List<StudentEntity>?> GetAllStudentsList()
    {
        List<StudentEntity> defaultResult = [];
        return await Consumer.GetAsync(Modules.Secretary, "students", defaultResult);
    }
}