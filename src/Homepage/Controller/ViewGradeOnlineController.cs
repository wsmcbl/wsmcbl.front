using Microsoft.JSInterop;
using wsmcbl.src.Components.ViewGradeOnline;
using wsmcbl.src.Controller.Services;

namespace wsmcbl.src.Controller;

public class ViewGradeOnlineController
{
    private ApiConsumer _apiConsumer { get; set; }
    
    public ViewGradeOnlineController(ApiConsumer apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }

    public async Task<StudentInfoDto?> GetInfoStudent(string studentId)
    {
        var resource = $"students/{studentId}";
        StudentInfoDto defaultResult = new StudentInfoDto();
        return await _apiConsumer.GetAsync(Modules.Academy, resource, defaultResult);
    }
    public async Task<(byte[]? Content, int? StatusCode)> GetGradePdf(string studentId, string token)
    {
        var resource = $"students/{studentId}/grades/export?token={token}";
        return await _apiConsumer.GetPdfAsync(Modules.Academy, resource);
    }
}