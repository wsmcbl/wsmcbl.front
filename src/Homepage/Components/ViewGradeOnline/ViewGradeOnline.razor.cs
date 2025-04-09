using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.Components.ViewGradeOnline;

public partial class ViewGradeOnline : ComponentBase
{
    [Inject] ViewGradeOnlineController Controller { get; set; } = null!;
    [Inject] private IJSRuntime Js { get; set; } = null!;
    private byte[]? GradePdf { get; set; } = [];
    private string? token {get;set;}
    private string? studentId {get;set;}
    private string? ErrorMessage { get; set; }


    private async Task GetGrade()
    {
        // Zona horaria del servidor: CDT (UTC-5)
        // Zona horaria de Nicaragua: (UTC-6) - 1 hora menos que el servidor

        // Fecha y hora objetivo en Nicaragua: Jueves 10 de abril a las 6:00 AM (UTC-6)
        // Esto equivale a 7:00 AM en el servidor (UTC-5)
        DateTime serverNow = DateTime.Today; // Hora actual del servidor (UTC-5)

        // Crear la fecha objetivo en la zona horaria de Nicaragua (UTC-6)
        DateTime targetDateInNicaragua = new DateTime(2025, 4, 10, 6, 0, 0); // 10/4/2025 6:00 AM UTC-6

        // Convertir la hora de Nicaragua a la hora del servidor (sumar 1 hora)
        DateTime targetDateInServerTime = targetDateInNicaragua.AddHours(1); // 10/4/2025 7:00 AM UTC-5

        if (serverNow >= targetDateInServerTime)
        {
            var (content, statusCode) = await Controller.GetGradePdf(studentId!, token!);
        
            if (statusCode == 200)
            {
                GradePdf = content;
                ErrorMessage = null;
                await Js.InvokeVoidAsync("mostrarModal", "PdfViewerModal");
            }
            else
            {
                ErrorMessage = statusCode switch
                {
                    400 => "Solicitud incorrecta. Por favor, revise los datos ingresados.",
                    401 => "No autorizado. Por favor, revise su código y contraseña.",
                    403 => "No tiene permisos para hacer esta consulta.",
                    404 => "El estudiante no fue encontrado.",
                    409 => "El estudiante no esta solvente.",
                    500 => "Inténtelo nuevamente más tarde.",
                    _ => "Ocurrió un error inesperado. Código: " + statusCode
                };
            
            }
        }
        else
        {
            ErrorMessage = "Las calificaciones estaran disponibles el dia 11 de abril de 2025.";
            Console.WriteLine($"Verificando fecha del servidor: {serverNow:dd/MM/yyyy}");
        }
        
    }
}