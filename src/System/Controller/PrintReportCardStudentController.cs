using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController(ApiConsumer consumer)
{
    public async Task<byte[]?> GetPdfContent(string studentId)
    {
        var resource = $"documents/report-cards/{studentId}";
        return await consumer.GetPdfAsync(Modules.Academy, resource);
    }
}