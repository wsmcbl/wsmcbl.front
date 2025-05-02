using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class CreateSubjectDataController : BaseController
{
    public CreateSubjectDataController(ApiConsumerFactory apiFactory) : base(apiFactory, "catalogs")
    {
    }
    
    public async Task<List<SubjectDataEntity>> GetSubjectList()
    {
        var defaultResult = new List<SubjectDataEntity>();
        var resource = $"{path}/subjects";
        return await apiFactory.WithNotificator.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    public async Task<List<DegreeDataEntity>> GetDegreeDataList()
    {
        var defaultResult = new List<DegreeDataEntity>();
        var resource = $"{path}/degrees";
        return await apiFactory.WithNotificator.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    public async Task<List<SubjectAreaEntity>> GetAreaList()
    {
        var defaultResult = new List<SubjectAreaEntity>();
        var resource = $"{path}/subjects/areas";
        return await apiFactory.WithNotificator.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    public async Task<bool> CreateSubjectData(SubjectDataEntity subject)
    {
        SubjectDto defaultResult = new();
        var resource = $"{path}/subjects";
        var response = await apiFactory.WithNotificator.PostAsync(Modules.Secretary, resource, subject, defaultResult);
        return response != defaultResult;
    }
    public async Task<bool> UpdateSubjectData(SubjectDataEntity subject)
    {
        var resource = $"{path}/subjects/{subject.subjectDataId}";
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, subject);
    }
}