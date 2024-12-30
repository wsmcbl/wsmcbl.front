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
        return await _apiConsumer.GetAsync(Modules.Academy, "enrollments/teachers", defaultResult);
    }
    
    public async Task<List<TeacherEntity>> GetActiveTeachers()
    {
        List<TeacherEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, "teachers", defaultResult);
    }

    
    public async Task<bool> UpdateTeacherGuide(ChangeTeacherDto teacher)
    {
        var resource = $"enrollments";
        var response = await _apiConsumer.PutAsync(Modules.Academy, resource, teacher);
        return response;    
    }
    
    public async Task<bool> UpdateTeacherSubject(TeacherSubjectDto teacher)
    {
        var resource = $"subjects";
        var response = await _apiConsumer.PutAsync(Modules.Secretary, resource, teacher);
        return response;    
    }
    
}