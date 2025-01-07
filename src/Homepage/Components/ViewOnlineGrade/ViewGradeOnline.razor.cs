using Microsoft.AspNetCore.Components;
using wsmcbl.homepage.Controller;

namespace wsmcbl.homepage.Components.ViewOnlineGrade;

public partial class ViewGradeOnline : ComponentBase
{
    [Inject] ViewGradeController Controller { get; set; } = null!;
    private byte[]? GradePdf { get; set; } = [];
    private string? token {get;set;}
    private string? studentId {get;set;}
    private string? ErrorMessage { get; set; }


    private async Task GetGrade()
    {
        var (content, statusCode) = await Controller.GetGrade(studentId, token);
        
        if (statusCode == 200)
        {
            GradePdf = content;
            ErrorMessage = null;
        }
        else
        {
            ErrorMessage = statusCode switch
            {
                400 => "Solicitud incorrecta. Por favor, revise los datos ingresados.",
                401 => "No autorizado. Por favor, revise su contraseña.",
                403 => "No tiene permisos para hacer esta consulta.",
                404 => "El estudiante no fue encontrado.",
                409 => "El estudiante no esta solvente.",
                500 => "Error del servidor. Inténtelo nuevamente más tarde.",
                _ => "Ocurrió un error inesperado. Código: " + statusCode
            };
            
        }
    }
}