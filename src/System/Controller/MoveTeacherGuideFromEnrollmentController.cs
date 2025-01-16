using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Components.MoveTeacherGuide;
using wsmcbl.src.View.Components.UpdateTeacherOfSubject;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.Controller;

public class MoveTeacherGuideFromEnrollmentController
{
    private ApiConsumerWithNotificator _apiConsumer;
    
    public MoveTeacherGuideFromEnrollmentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<List<TeacherEntity>> GetTeacherNoGuide()
    {
        List<TeacherEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy, "teachers?q=non-guided", defaultResult);
    }
    
    public async Task<List<TeacherEntity>> GetActiveTeachers()
    {
        List<TeacherEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy, "teachers?q=active", defaultResult);
    }

    
    public async Task<bool> UpdateTeacherGuide(string enrollmentId, string teacherId)
    {
        var resource = $"enrollments/{enrollmentId}/teachers?teacherId={teacherId}";
        return await _apiConsumer.PutAsync<object>(Modules.Academy, resource, null);
    }
    
    public async Task<bool> UpdateTeacherSubject(string enrollmentId, string subjectId, string teacherId)
    {
        var resource = $"enrollments/{enrollmentId}/subjects/{subjectId}?teacherId={teacherId}";
        return await _apiConsumer.PutAsync<object>(Modules.Academy, resource, null);
    }
    
}
