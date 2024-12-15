using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.Controller;

public class EditStudentController
{
    private ApiConsumerWithNotificator _apiConsumer;
    public EditStudentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<StudentFullDto> GetStudentData(string? studentId)
    {
        var resource = $"students/{studentId}"; 
        var defaultResult = new StudentFullDto();
        return await _apiConsumer.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    
    
    
}