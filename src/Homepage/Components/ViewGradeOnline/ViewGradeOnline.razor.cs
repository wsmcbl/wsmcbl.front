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
                401 => "No autorizado. Por favor, revise su contraseña.",
                403 => "No tiene permisos para hacer esta consulta.",
                404 => "El estudiante no fue encontrado.",
                409 => "El estudiante no esta solvente.",
                500 => "Inténtelo nuevamente más tarde.",
                _ => "Ocurrió un error inesperado. Código: " + statusCode
            };
            
        }
    }
}