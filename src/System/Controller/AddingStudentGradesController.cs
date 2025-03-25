using Microsoft.JSInterop;
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
    private readonly IJSRuntime _jsRuntime;

    public AddingStudentGradesController(ApiConsumerFactory apiConsumerFactory, LoginController loginController, Notificator notificator, IJSRuntime jsRuntime)
    {
        _apiConsumerFactory = apiConsumerFactory;
        _loginController = loginController;
        _notificator = notificator;
        _jsRuntime = jsRuntime;
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
            result = await _apiConsumerFactory.Default.GetAsync(Modules.Academy, resource, result);

            result.DeleteWithoutGrades();
            result.UpdateStudentGradeList();
        }
        catch (InternalException e)
        {
            if (e.StatusCode == 409)
            {
                await _notificator.ShowInformation("El registro de calificaciones no está activo en este momento. " +
                                             "Por favor, inténtelo más tarde.");
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

    public async Task<List<DegreeEntity>> GetSortedDegreeList()
    {
        
        var createEnrollmentController = new CreateEnrollmentController(_apiConsumerFactory);
        var result = await createEnrollmentController.GetDegreeList(new PagedRequest(20));
        return result.data.Where(e => e.quantity > 0).OrderBy(e => e.position).ToList();
    }
    
    public async Task GetGradeDocument(string teacherId, string enrollmentId, int partialId, string EnrollmentName)
    {
        var fileBytes = await _apiConsumerFactory.WithNotificator.GetBackupAsync(Modules.Academy, $"teachers/{teacherId}/enrollments/{enrollmentId}/documents?partialId={partialId}");
        if (fileBytes.Length > 0)
        {
            var fileName = $"Registro de calificaciones de {EnrollmentName}.xlsx";
            var base64 = Convert.ToBase64String(fileBytes);
            var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
            await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
        }
    }
    
}