using System.Text.Json;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent;
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
        var resource = $"students?q=one%3A{studentId}"; 
        var defaultResult = new StudentFullDto();
        return await _apiConsumer.GetAsync(Modules.Secretary, resource, defaultResult);
    }
    
     public async Task<bool> UpdateStudentData(StudentEntity? student)
    {
        var resource = $"students";
        var content = MapperStudent.ToStudentFullDto(student);
        var response = await _apiConsumer.PutAsync(Modules.Secretary, resource, content);
        return response;    
    }
    
    
    
}