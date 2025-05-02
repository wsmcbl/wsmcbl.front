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
    
    public async Task<bool> CreateSubjectData(SubjectDataEntity subject)
    {
        SubjectDto Default = new();
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, path, subject, Default);
        return response != Default;
    }
    
    public async Task<List<DegreeDataEntity>> GetDegreeDataList()
    {
        var Default = new List<DegreeDataEntity>();

        var resource = $"{path}/degrees";
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, Default);
    }
}