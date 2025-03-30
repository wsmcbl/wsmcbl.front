using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.Controller;

public class UpdateStudentController : BaseController
{
    public UpdateStudentController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }

    public async Task<StudentEntity> GetStudentById(string? studentId)
    {
        var resource = $"{path}/{studentId}";
        
        var result = await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary, resource, new StudentFullDto());

        return result.ToEntity();
    }

    public async Task<bool> UpdateStudentData(StudentEntity student, bool generateToken)
    {
        var resource = $"{path}/{student.studentId}?withNewToken={generateToken}";
        
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, student.MapToDto());
    }

    public async Task<bool> UpdatePicture(string studentId, MultipartFormDataContent content)
    {
        var resource = $"{path}/{studentId}/pictures";
        
        return await apiFactory.WithNotificator.PutPhotoAsync(Modules.Secretary, resource, content);
    }
}