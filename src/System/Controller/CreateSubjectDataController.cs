using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class CreateSubjectDataController
{
    private readonly ApiConsumerFactory apiFactory;
    
    public CreateSubjectDataController(ApiConsumerFactory apiFactory)
    {
        this.apiFactory = apiFactory;
    }
    
    public async Task<bool> CreateSubjectData(SubjectDto subject)
    {
        var resource = "schoolyears/subjects";
        SubjectDto Default = new();
        var response = await apiFactory.Default.PostAsync(Modules.Secretary, resource, subject, Default);
        return response != Default;
    }
}