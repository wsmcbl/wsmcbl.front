using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.PerformanceReportBySection;
using wsmcbl.src.View.Academy.TopStudents;

namespace wsmcbl.src.Controller;

public class GeneratePerformanceReportBySection : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Notificator _notificator;
    private readonly GetSchoolYearServices _schoolYearServices;
    public GeneratePerformanceReportBySection(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime, Notificator notificator, GetSchoolYearServices getSchoolYearServices) : base(apiFactory, "teachers")
    {
        _jsRuntime = jsRuntime;
        _notificator = notificator;
        _schoolYearServices = getSchoolYearServices;
    }

    public async Task<List<StudentsGradeSumaryDto>> GetStudentsGradeSummary(string teacherId, int partialId)
    {
        List<StudentsGradeSumaryDto> defaultResult = [];
        return await apiFactory.WithNotificator.GetAsync(Modules.Academy,
            $"{path}/{teacherId}/enrollments/guide/grades/summary?partialId={partialId}", defaultResult);
    }

    public async Task GetEnrollmentGradeByTeacherXlsx(string teacherId, int partialId, string partialLabel, string enrollmentName)
    {
        var resource = $"{path}/{teacherId}/enrollments/guide/grades/summary/export?partialId={partialId}";
        
        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            await _notificator.ShowError("No se realizó la descarga del archivo. \n\n Es posible que no existan registros para el parcial seleccionado.");
            return;
        }

        var schoolYearLabel = await _schoolYearServices.GetSchoolYearActiveLabel();
        var nameGenerator = new EnrollmentFileNameGenerator(enrollmentName, schoolYearLabel);
        var fileName = nameGenerator.GetFileName(partialLabel, "sabana");
        
        
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