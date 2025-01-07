using wsmcbl.src.Controller.Services;

namespace wsmcbl.src.Controller;

public class ViewGradeController
{
    
    private ApiConsumer _apiConsumer;
    public ViewGradeController(ApiConsumer apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<(byte[]? Content, int? StatusCode)> GetGrade(string? studentId, string? token)
    {
        var resource = $"grades/{studentId}?token={token}"; 

        try
        {
            var content = await _apiConsumer.GetPdfAsync(Modules.Secretary, resource); 
            
            if (content.Length > 0)
            {
                return (content, 200);
            }
            return (null, 500);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode.HasValue)
            {
                return (null, (int)ex.StatusCode.Value);
            }
            throw;
        }
    }
    
}