using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Components.ViewGradeOnline;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Components.ViewHomeStudent;

public partial class HomeStudents : ComponentBase
{
    [Inject] ViewGradeOnlineController Controller { get; set; } = null!;
    [Inject] HomeNavigator homeNavigator { get; set; } = null!;
    [Inject] IJSRuntime _jsRuntime { get; set; } = null!;
    [Parameter] public string StudentId { get; set; } = string.Empty;
    [Parameter] public string Token { get; set; } = string.Empty;
    private StudentInfoDto? Student { get; set; } = null!;
    private byte[]? GradePdf { get; set; } = [];
    private string? ErrorMessage { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (StudentId != string.Empty && Token != string.Empty)
        {
            Student = await Controller.GetInfoStudent(StudentId);
        }
    }
    
    private async Task DownloadGrades()
    {
        var (content, statusCode) = await Controller.GetGradePdf(StudentId!, Token!);

        if (statusCode == 200 && content != null)
        {
            if (GradePdf != null)
            {
                var base64 = Convert.ToBase64String(content);
                var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
                await _jsRuntime.InvokeVoidAsync("downloadFile", Student!.studentName, url);
            }

            ErrorMessage = null;
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
}