using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Controller;

public class GeneratePerformanceReportBySection : BaseController
{
    private readonly IJSRuntime _jsRuntime;

    public GeneratePerformanceReportBySection(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime) : base(apiFactory, "teachers/")
    {
        _jsRuntime = jsRuntime;
    }


    public async Task GetEnrollmentGradeByTeacherXlsx(string teacherId, int partialId, string enrollmentName)
    {

        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, 
            path+$"{teacherId}/enrollments/guide/grades/summary/export?partial={partialId}");
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("No se realizó la descarga del archivo.");
        }

        var fileName = $"{partialId} Parcial {enrollmentName}.xlsx";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }
}