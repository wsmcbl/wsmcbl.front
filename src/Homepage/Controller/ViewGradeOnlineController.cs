using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class ViewGradeOnlineController
{
    private ApiConsumer _apiConsumer;
    public ViewGradeOnlineController(ApiConsumer apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<byte[]> GetGradeByStudent(string? studentId, string? token)
    {
        var resource = $"grades/{studentId}?token={token}";
        return await _apiConsumer.GetPdfAsync(Modules.Academy, resource);
    }
    
    
}