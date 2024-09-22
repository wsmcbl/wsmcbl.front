using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController(ApiConsumer consumer)
{
    public async Task<byte[]?> GetPdfContent(string studentId)
    {
        var resource = $"documents/report-cards/{studentId}";
        byte[] defaultResult = [];
        var content = await consumer.GetPdfAsync(Modules.Academy, resource, defaultResult);
        return content;
    }
}