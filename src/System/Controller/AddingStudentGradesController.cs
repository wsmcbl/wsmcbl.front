using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.AddGrade;
using wsmcbl.src.View.Academy.EnrollmentListByTeacher;

namespace wsmcbl.src.Controller;

public class AddingStudentGradesController
{
    private readonly Notificator _notificator;
    private readonly LoginController _loginController;
    private readonly ApiConsumerFactory _apiConsumerFactory;

    public AddingStudentGradesController(ApiConsumerFactory apiConsumerFactory, LoginController loginController, Notificator notificator)
    {
        _apiConsumerFactory  = apiConsumerFactory;
        _loginController = loginController;
        _notificator = notificator;
    }

    public async Task<List<PartialEntity>> GetPartialList()
    {
        List<PartialEntity> defaultResult = [];
        return await _apiConsumerFactory.WithNotificator.GetAsync(Modules.Academy, "partials", defaultResult);
    }

    public async Task<TeacherEntity> GetTeacherById(string teacherId)
    {
        var defaultResult = new TeacherDto();
        var result = await _apiConsumerFactory.WithNotificator.GetAsync(Modules.Academy, $"teachers/{teacherId}", defaultResult);

        return result.toEntity();
    }

    public async Task<List<EnrollmentByTeacherDto>> GetEnrollmentList(string teacherId)
    {
        List<EnrollmentByTeacherDto> defaultResult = [];
        var resource = $"teachers/{teacherId}/enrollments";
        return await _apiConsumerFactory.WithNotificator.GetAsync(Modules.Academy, resource, defaultResult);
    }

    public async Task<FullEnrollmentDto> GetEnrollment(string teacherId, string enrollmentId, int partialId)
    {
        var result = new FullEnrollmentDto();
        result.Init();

        try
        {
            var resource = $"teachers/{teacherId}/enrollments/{enrollmentId}?partialId={partialId}";
            result = await _apiConsumerFactory.Dafault.GetAsync(Modules.Academy, resource, result);

            result.DeleteWithoutGrades();
            result.UpdateStudentGradeList();
        }
        catch (InternalException e)
        {
            if (e.StatusCode == 404)
            {
                await _notificator.ShowInformation("El registro de calificaciones no está activo en este momento.\n" +
                                             " Por favor, inténtelo más tarde.");
            }
        }
        catch (Exception)
        {
            await _notificator.ShowError("Ha ocurrido un error el servidor.");
        }

        return result;
    }

    public async Task<bool> UpdateGrade(string teacherId, string enrollmentId, int partialId,
        List<GradeEntity> gradeList)
    {
        var resource = $"teachers/{teacherId}/enrollments/{enrollmentId}?partialId={partialId}";
        return await _apiConsumerFactory.WithNotificator.PutAsync(Modules.Academy, resource, gradeList);
    }

    public async Task<string> GetTeacherId()
    {
        return await _loginController.getRoleIdFromToken();
    }
}