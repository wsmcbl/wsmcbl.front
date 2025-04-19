using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintDocumentByStudentController : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    
    public PrintDocumentByStudentController(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime) : base(apiFactory, "students")
    {
        _jsRuntime = jsRuntime;
    }

    public async Task GetCertificate(string studentId, string studentName)
    {
        var resource = $"{path}/{studentId}/active-certificate/export";

        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("Error al descargar el archivo.");
        }

        var docname = $"Certificado de alumno activo de {studentName}.pdf";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/pdf;base64,{base64}";

        await _jsRuntime.InvokeVoidAsync("downloadFile", docname, url);
    }


    public async Task GetProforma(string studentId)
    {
        var resource = $"{path}/proforma/export?studentId={studentId}";

        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("Error al descargar el archivo.");
        }

        var docname = $"Proforma.pdf";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/pdf;base64,{base64}";

        await _jsRuntime.InvokeVoidAsync("downloadFile", docname, url);
    }
}