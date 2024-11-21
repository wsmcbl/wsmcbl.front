using Microsoft.AspNetCore.Mvc;

namespace wsmcbl.src.Controller.Service;

public abstract class ApiProblemDetails : ProblemDetails
{
    public Dictionary<string, List<string>>? Errors { get; set; } = new();
    
    public string GetValidationErrors()
    {
        if (Errors == null || Errors.Count == 0)
        {
            return string.Empty;
        }

        var result = "--- ";

        foreach (var item in Errors)
        {
            foreach (var message in item.Value)
            {
                result = $"{result}{item.Key}: {message} --- ";
            }
        }

        return result;
    }
}