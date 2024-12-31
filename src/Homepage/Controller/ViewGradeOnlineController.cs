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
    
    public async Task<(byte[]? Content, int? StatusCode)> GetGrade(string? studentId, string? token)
    {
        var resource = $"grades/{studentId}?token={token}";

        try
        {
            var content = await _apiConsumer.GetPdfAsync(Modules.Academy, resource);
            return (content, 200);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode.HasValue)
            {
                return (null, (int)ex.StatusCode.Value);
            }

            throw;
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    
}