using System.Text.Json;
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

    public async Task<List<EnrollmentByTeacherDto>> GetEnrollmentByTeacherId(string? teacherId)
    {
        List<EnrollmentByTeacherDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,$"enrollments/{teacherId}",defaultResult);
    } 
    
    public async Task<List<PartialEntity>> GetPartialsList()
    {
        List<PartialEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,"partials",defaultResult);
    }
    
    public async Task<(List<StudentEntity> studentList, List<SubjectEntity> subjectList)> GetFullInformationOfEnrollment(TeacherEnrollmentByPartialDto dto)
    {
        var defaultResult = new FullInformationOfEnrollmentDto();
        var result = await _apiConsumer.GetAsync(Modules.Academy, "enrollments", dto, defaultResult);

        result.updateStudentGradeList();
        
        return (result.studentList, result.subjectList);
    } 
    
    public async Task<bool> UpdateGrade(SaveGradeDto grade)
    {
        var json = JsonSerializer.Serialize(grade);
        return await _apiConsumer.PutAsync(Modules.Academy,"enrollments/subjects/grades",grade);
    }
    
    
    
}