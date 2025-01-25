using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Academy.AddGrade;
using wsmcbl.src.View.Academy.EnrollmentListByTeacher;

namespace wsmcbl.src.Controller;

public class AddingStudentGradesController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly LoginController _loginController;
    public AddingStudentGradesController(ApiConsumerWithNotificator apiConsumer, LoginController loginController)
    {
        _apiConsumer = apiConsumer;
        _loginController = loginController;
    } 
    
    public async Task<List<PartialEntity>> GetPartialList()
    {
        List<PartialEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Academy,"partials", defaultResult);
    }

    public async Task<TeacherEntity> GetTeacherById(string teacherId)
    {
        var defaultResult = new TeacherEntity();
        return await _apiConsumer.GetAsync(Modules.Academy, $"teachers/{teacherId}", defaultResult);
    }

    public async Task<List<EnrollmentByTeacherDto>> GetEnrollmentList(string teacherId)
    {
        List<EnrollmentByTeacherDto> defaultResult = [];
        var resource = $"teachers/{teacherId}/enrollments";
        return await _apiConsumer.GetAsync(Modules.Academy, resource, defaultResult);
    }
    
    public async Task<FullEnrollmentDto> GetEnrollment(string teacherId, string enrollmentId, int partialId)
    {
        var defaultResult = new FullEnrollmentDto();
        var resource = $"teachers/{teacherId}/enrollments/{enrollmentId}?partialId={partialId}";
        var result = await _apiConsumer.GetAsync(Modules.Academy, resource, defaultResult);

        result.DeleteWithoutGrades();
        result.UpdateStudentGradeList();

        return result;
    }

    public async Task<bool> UpdateGrade(string teacherId, string enrollmentId, int partialId, List<GradeEntity> gradeList)
    {
        var resource = $"teachers/{teacherId}/enrollments/{enrollmentId}?partialId={partialId}";
        return await _apiConsumer.PutAsync(Modules.Academy, resource, gradeList);
    }

    public async Task<string> getTeacherId()
    {
        return await _loginController.getRoleIdFromToken();
    }
}