using Microsoft.AspNetCore.Mvc;

namespace wsmcbl.src.Controller.Service;

public class ApiProblemDetails : ProblemDetails
{
    public Dictionary<string, List<string>>? Errors { get; set; } = new();
    
    public string GetValidationErrors()
    {
        if (Errors == null || Errors.Count == 0)
        {
            return string.Empty;
        }
        var result = "<div>Validation errors occurred<br><ul>";
        foreach (var item in Errors)
        {
            foreach (var message in item.Value)
            {
                result = $"{result}<li>{item.Key}: {message}</li> ";
            }
        }

        return $"{result}</ul><div>";
    }
}