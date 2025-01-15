using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.AddGrade;
using wsmcbl.src.View.Academy.EnrollmentListByTeacher;

namespace wsmcbl.src.Controller;

public class AddStudentGradeController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly LoginController _loginController;
    public AddStudentGradeController(ApiConsumerWithNotificator apiConsumer, LoginController loginController)
    {
        _apiConsumer = apiConsumer;
        _loginController = loginController;
    }

    public async Task<List<EnrollmentByTeacherDto>> GetEnrollmentByTeacherId(string? teacherId)
    {
        List<EnrollmentByTeacherDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,$"enrollments/{teacherId}",defaultResult);
    } 
    
    public async Task<List<PartialEntity>> GetPartialsList()
    {
        List<PartialEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,"partials", defaultResult);
    }
    
    public async Task<FullInformationOfEnrollmentDto> GetFullEnrollment(TeacherEnrollmentByPartialDto dto)
    {
        var defaultResult = new FullInformationOfEnrollmentDto();
        var result = await _apiConsumer.GetAsync(Modules.Academy, "enrollments", dto, defaultResult);

        result.updateStudentGradeList();

        return result;
    }

    public async Task<bool> UpdateGrade(TeacherEnrollmentByPartialDto teacherEnrollment, List<GradeEntity> gradeList)
    {
        var data = new SaveGradeDto
        {
            teacherEnrollment = teacherEnrollment,
            gradeList = gradeList
        };
        
        return await _apiConsumer.PutAsync(Modules.Academy, "enrollments/subjects/grades", data);
    }

    public async Task<string> getTeacherId()
    {
        return await _loginController.getRoleIdFromToken();
    }

    public async Task<string?> getTeacherName(string teacherId)
    {
        var controller = new MoveTeacherGuideFromEnrollmentController(_apiConsumer);
        
        var teacherList = await controller.GetActiveTeachers();
        if (teacherList == null || teacherList.Count == 0)
        {
            throw new InternalException("No there teachers active.");
        }

        var result = teacherList.FirstOrDefault(e => e.teacherId == teacherId);
        if (result == null)
        {
            throw new InternalException($"Teacher entity with id ({teacherId}) not foud.");
        }
        
        return result.fullName;
    }
}