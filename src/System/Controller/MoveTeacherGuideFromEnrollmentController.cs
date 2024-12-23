using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.MoveTeacherGuide;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.Controller;

public class MoveTeacherGuideFromEnrollmentController
{
    private ApiConsumerWithNotificator _apiConsumer;
    
    public MoveTeacherGuideFromEnrollmentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<List<TeacherNoGuideDto>> GetTeacherNoGuide()
    {
        List<TeacherNoGuideDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy, "enrollments/teachers", defaultResult);
    }
    
    public async Task<bool> UpdateTeacherGuide(ChangeTeacherDto teacher)
    {
        var resource = $"enrollments";
        var response = await _apiConsumer.PutAsync(Modules.Academy, resource, teacher);
        return response;    
    }
}