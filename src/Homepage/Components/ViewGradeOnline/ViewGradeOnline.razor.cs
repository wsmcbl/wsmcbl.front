using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Components.ViewGradeOnline;

public partial class ViewGradeOnline : ComponentBase
{
    [Inject] ViewGradeOnlineController Controller { get; set; } = null!;
    [Inject] HomeNavigator homeNavigator { get; set; } = null!;
    private StudentInfoDto? Student { get; set; }
    private byte[]? GradePdf { get; set; } = [];
    private string? token { get; set; }
    private string studentId { get; set; } = string.Empty;
    private string? ErrorMessage { get; set; }


    private async Task GetGrade()
    {
        var serverNow = DateTime.UtcNow; // UTC
        var targetDateInServerTime = new DateTime(2025, 4, 11, 12, 35, 0); 
        // UTC: 11/04/2025, 12:35 PM    UTC-6: 11/04/2025, 6:35 AM
        
        if (serverNow < targetDateInServerTime)
        {
            ErrorMessage = "Las calificaciones estarán disponibles el día 11 de abril de 2025, a partir de las 6:35 AM.";
            Console.WriteLine($"Verificando fecha del servidor: {serverNow:dd/MM/yyyy}");
            return;
        }
        
        try
        {
            if (studentId != string.Empty && token != string.Empty)
            {
                Student = await Controller.GetInfoStudent(studentId!);
            }
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
        }
        
        
        if (Student != null && !token.IsNullOrEmpty())
        { 
            homeNavigator.ToPage($"/online-grades/{studentId}/{token}");
        }
        
    }
}