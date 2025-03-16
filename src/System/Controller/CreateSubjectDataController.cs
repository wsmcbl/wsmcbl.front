using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class CreateSubjectDataController : BaseController
{
    public CreateSubjectDataController(ApiConsumerFactory apiFactory) : base(apiFactory, "catalogs")
    {
    }
    
    public async Task<bool> CreateSubjectData(SubjectDto subject)
    {
        SubjectDto Default = new();
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, resource, subject, Default);
        return response != Default;
    }
}