using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Academy.PorcentageSubjectEvaluated;
using wsmcbl.src.View.Academy.SubjectByTeacher;

namespace wsmcbl.src.Controller;

public class TeacherDashboardController : BaseController
{
    public TeacherDashboardController(ApiConsumerFactory apiConsumerFactory) : base(apiConsumerFactory, "teachers/")
    {
    }

    public async Task<List<SubjectByTeacherDto>> GetSubjectTeacherListById(string teacherId)
    {
        List<SubjectByTeacherDto> defaultResult = [];
        var resource = $"{path}{teacherId}/subjects";
        return await apiFactory.WithNotificator.GetAsync(Modules.Academy, resource, defaultResult);
    }

    public async Task<List<SubjectStats>> GetSubjectEvaluated(string teacherId)
    {
        List<SubjectStats> defaultResult = [];
        var resource = $"{path}{teacherId}/subjects/percentage-evaluated";
        return await apiFactory.WithNotificator.GetAsync(Modules.Academy, resource, defaultResult);
    }
}