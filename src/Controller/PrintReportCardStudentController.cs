using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.StudentPasswordView;

namespace wsmcbl.src.Controller;

public class PrintReportCardStudentController : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    public PrintReportCardStudentController(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime) : base(apiFactory, "students")
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<byte[]> GetDegreeWhitAdminToken(string studentId, string token)
    {
        var resource = $"students/{studentId}/report-card/export?adminToken={token}";
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task<byte[]> GetDegreeWhitStudentToken(string studentId, string studentToken)
    {
        var resource = $"students/{studentId}/grades/export?token={studentToken}";
        return await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
    }
    
    public async Task<StudentDetailsDto> GetFullInfoStudent(string studentId, string token)
    {
        var resource = $"students/{studentId}?token={token}";
        return await apiFactory.Default.GetAsync(Modules.Academy, resource, new StudentDetailsDto());
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