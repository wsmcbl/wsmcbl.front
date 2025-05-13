using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    public PrintReportCardStudentController(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime) : base(apiFactory, "students")
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<byte[]> GetDegreeWhitToken(string studentId, string token)
    {
        var resource = $"students/{studentId}/report-card/export?adminToken={token}";
       
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task<byte[]> GetDegree(string studentId)
    {
        var resource = $"students/{studentId}/report-card/export";
        return await apiFactory.Default.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task GetAcademicReportBySchoolYear(string studentId, string studentName, string schoolYearId, string schoolYearName)
    {
        var resource = $"students/academic/record/export?studentId={studentId}&schoolyearId={schoolYearId}";
        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("No se realizó la descarga del archivo.");
        }
        var fileName = $"Historial Academico de {studentName} año {schoolYearName}.pdf";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/pdf;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }

}