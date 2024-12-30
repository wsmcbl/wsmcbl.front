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
    
    public async Task<(List<StudentDto> studentList, List<SubjectsDto> subjectList, List<GradesOfEnrollmentsDto> subjectPartialList)> GetFullInformationOfEnrollment(TeacherEnrollmentByPartialDto dto)
    {
        var defaultResult = new FullInformationofEnrollmentDto();
        var result = await _apiConsumer.GetAsync(Modules.Academy, "enrollments", dto, defaultResult);
        return (result.studentList, result.subjectList, result.subjectPartialList);
    } 
    
    
    
    
    
}