using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Academy.EnrollmentGuide;
using wsmcbl.src.View.Secretary.Schoolyear.SubjectData;

namespace wsmcbl.src.Controller;

public class CreateSubjectDataController : BaseController
{
    public CreateSubjectDataController(ApiConsumerFactory apiFactory) : base(apiFactory, "catalogs")
    {
    }
    
    public async Task<List<SubjectDataEntity>> GetSubjectDataList()
    {
        var Default = new List<SubjectDataEntity>();

        var resource = $"{path}/subjects";
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, Default);
    }
    
    public async Task<bool> CreateSubjectData(SubjectDataEntity subject)
    {
        SubjectDto Default = new();
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, path, subject, Default);
        return response != Default;
    }
    
    public async Task<List<BasicDegreeDto>> GetDegreeDataList()
    {
        var Default = new List<BasicDegreeDto>();

        var resource = $"{path}/degrees";
        return await apiFactory.Default.GetAsync(Modules.Secretary, resource, Default);
    }
}