using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.Controller;

public class UpdateStudentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    public UpdateStudentController(ApiConsumerWithNotificator apiConsumer)
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
        var content = MapperStudent.ToStudentFullDto(student);
        return await _apiConsumer.PutAsync(Modules.Secretary, "students", content);
    }
    
    
    
}