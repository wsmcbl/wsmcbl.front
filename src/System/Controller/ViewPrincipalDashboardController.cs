using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.Charts;
using wsmcbl.src.View.Management.ReportUserCalification;

namespace wsmcbl.src.Controller;

public class ViewPrincipalDashboardController : BaseController
{
    private readonly IJSRuntime _jsRuntime;
    public ViewPrincipalDashboardController(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime) : base(apiFactory, string.Empty)
    {
        _jsRuntime = jsRuntime;
    }
    
    public async Task<DistributionDto> GetCurrentDistribution()
    {
        DistributionDto defaultResult = new();
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, "students/distributions", defaultResult);
    }

    public async Task<RevenuesDto> GetCurrentRevenues()
    {
        RevenuesDto defaultResult = new();
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, "revenues", defaultResult);
    }
    
    public async Task<List<TeacherReportDto>?> GetTeacherGradeReports()
    {
        List<TeacherReportDto> defaultResult = [];
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, "teachers/grades/summaries", defaultResult);
    }

    public async Task<List<SubjectsDto>> GetSubjectOfSchoolYear()
    {
        List<SubjectsDto> defaultResult = [];
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, "subjects", defaultResult);
    }
    
    public async Task GetReportOfEnrollment(string enrollmentId, int partialId, string name)
    {
        var resource = $"enrollments/{enrollmentId}/grades/export?partial={partialId}";
        
        var fileBytes = await apiFactory.WithNotificator.GetByteFileAsync(Modules.Management, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("No se realizó la descarga del archivo.");
        }
        
        var fileName = $"{name}.xlsx";
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }
}