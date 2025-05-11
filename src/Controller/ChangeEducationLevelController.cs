using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.StudentList.ModifyEducationLevel;

namespace wsmcbl.src.Controller;

public class ChangeEducationLevelController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    public ChangeEducationLevelController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<StudentForEducationLevelDto> GetStudent(string? studentId)
    {
        StudentForEducationLevelDto Default = new();
        return await _apiConsumer.GetAsync(Modules.Secretary, $"students/levels?studentId={studentId}", Default);
    }

    public async Task<bool> ChangeEducationLevel(string studentId, int level)
    {
        StudentForEducationLevelDto Default = new();
        return await _apiConsumer.PutAsync(Modules.Secretary, $"students/levels?studentId={studentId}&level={level}", Default);
    }
    
    
}