using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.Controller;

public class UpdateStudentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public UpdateStudentController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }

    public async Task<StudentEntity> GetStudentById(string? studentId)
    {
        var resource = $"students/{studentId}";
        var defaultResult = new StudentFullDto();
        var result = await _apiConsumer.GetAsync(Modules.Secretary, resource, defaultResult);

        return result.ToEntity();
    }

    public async Task<bool> UpdateStudentData(StudentEntity student, bool generateToken, string studentId)
    {
        var resource = $"students/{studentId}?withNewToken={generateToken}";
        return await _apiConsumer.PutAsync(Modules.Secretary, resource, student.MapToDto());
    }

    public async Task<bool> SaveProfile(string studentId, MultipartFormDataContent content)
    {
        return await _apiConsumer.PutPhotoAsync(Modules.Secretary, $"students/{studentId}/pictures", content);
    }
}