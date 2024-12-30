using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Academy.AddGrade;
using wsmcbl.src.View.Academy.ListOfEnrollmentByTeacher;

namespace wsmcbl.src.Controller;

public class AddStudentGradeController
{
    private ApiConsumerWithNotificator _apiConsumer;
    public AddStudentGradeController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }

    public async Task<List<EnrollmentByTeacherDto>> GetEnrollmentByTeacherId(string teacherId)
    {
        List<EnrollmentByTeacherDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,$"enrollments/{teacherId}",defaultResult);
    } 
    
    public async Task<List<PartialEntity>> GetPartialsList()
    {
        List<PartialEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,"partials",defaultResult);
    }

    public async Task<FullInformationofEnrollmentDto> GetFullInformationOfEnrollment(GetFullInformationDto dto)
    {
        var defaultResult = new FullInformationofEnrollmentDto();
        var a = await _apiConsumer.GetWithDtoAsync(Modules.Academy, "enrollments", dto, defaultResult);
        return a;
    } 
    
    
    
    
    
}