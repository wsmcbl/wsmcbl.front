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
    
    public async Task<StudentEntity> GetStudentById(string? studentId)
    {
        var resource = $"students?q=one%3A{studentId}"; 
        var defaultResult = new StudentFullDto();
        var result = await _apiConsumer.GetAsync(Modules.Secretary, resource, defaultResult);
        
        return new StudentEntity(result); 
    }
    
     public async Task<bool> UpdateStudentData(StudentEntity? student)
    {
        var content = MapperStudent.ToStudentFullDto(student);
        return await _apiConsumer.PutAsync(Modules.Secretary, "students", content);
    }
}