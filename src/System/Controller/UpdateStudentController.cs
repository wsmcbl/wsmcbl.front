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
        var resource = $"students?q=one%3A{studentId}";
        var defaultResult = new StudentFullDto();
        var result = await _apiConsumer.GetAsync(Modules.Secretary, resource, defaultResult);

        return result.ToEntity();
    }

    public async Task<bool> UpdateStudentData(StudentEntity student, bool generateToken)
    {
        var resource = $"students?withNewToken={generateToken}";
        return await _apiConsumer.PutAsync(Modules.Secretary, resource, student.MapToDto());
    }
}