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
            var (pdf, statusCode) = await _apiConsumer.GetPdfAsync(Modules.Academy, resource); 
            
            if (pdf != null && pdf.Length > 0)
            {
                return (pdf, 200);
            }
            return (null, statusCode);
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