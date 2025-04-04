using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.Charts;
using wsmcbl.src.View.Management.ReportUserCalification;

namespace wsmcbl.src.Controller;

public class ViewPrincipalDashboardController : BaseController
{
    public ViewPrincipalDashboardController(ApiConsumerFactory apiFactory) : base(apiFactory, string.Empty)
    {
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
}