using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.PerformanceReportBySection;
using wsmcbl.src.View.Academy.TopStudents;

namespace wsmcbl.src.Controller;

public class GeneratePerformanceReportBySection : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    private Notificator Notificator { get; set; }

    public GeneratePerformanceReportBySection(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime, Notificator notificator) : base(apiFactory, "teachers")
    {
        _jsRuntime = jsRuntime;
        Notificator = notificator;
    }

    public async Task<List<StudentsGradeSumaryDto>> GetStudentsGradeSummary(string teacherId, int partialId)
    {
        List<StudentsGradeSumaryDto> defaultResult = [];
        return await apiFactory.WithNotificator.GetAsync(Modules.Academy,
            $"{path}/{teacherId}/enrollments/guide/grades/summary?partialId={partialId}", defaultResult);
    }

    public async Task GetEnrollmentGradeByTeacherXlsx(string teacherId, int partialId, string enrollmentName)
    {
        var resource = $"{path}/{teacherId}/enrollments/guide/grades/summary/export?partialId={partialId}";
        
        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            await Notificator.ShowError("No se realizó la descarga del archivo.");
            return;
        }

        // $$$$$$$$$$$$$$$$$$$$$$$$$$$$$ REPARAR SCHOOLYEAR Y PARTIAL $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        var NameGenerator = new EnrollmentFileNameGenerator(enrollmentName, "2025");
        var fileName = NameGenerator.GetFileName(1, "sabana");
        
        
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }

    public async Task<List<TopStudentsDto>?> GetTopStudents(string teacherId, int partialId)
    {
        List<TopStudentsDto> defaultResult = [];
        return await apiFactory.WithNotificator.GetAsync(Modules.Academy,
            $"{path}/{teacherId}/enrollments/guide/performance?partialId={partialId}", defaultResult);    }
}