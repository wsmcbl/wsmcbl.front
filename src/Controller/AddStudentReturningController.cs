using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.AddGradeToStudentReturning;

namespace wsmcbl.src.Controller;

public class AddStudentReturningController(ApiConsumerFactory apiConsumerFactory)
{
    private readonly ApiConsumerWithNotificator _apiConsumer = apiConsumerFactory.WithNotificator;
    
    public async Task<List<DetailForSubjectAddGradeReturning>> GetSubjectInfo(string enrollmentId, int? partialId)
    {
        var resource = $"enrollments/{enrollmentId}/partials/{partialId}/subjects";
        return await _apiConsumer.GetAsync(Modules.Academy, resource, new List<DetailForSubjectAddGradeReturning>());
    }
    
    public async Task<bool> Save(GradeDto data)
    {
        var resource = $"grades/student/returning";
        GradeDto defaultdata = new();
        var response = await _apiConsumer.PostAsync(Modules.Academy, resource, data, defaultdata);
        if (response != defaultdata)
        {
            return true;
        }
        return false;
    }
}