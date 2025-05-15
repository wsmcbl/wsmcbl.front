using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.StudentPasswordView;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.Controller;

public class UpdateStudentController : BaseController
{
    public UpdateStudentController(ApiConsumerFactory apiFactory) : base(apiFactory, "students")
    {
    }
    
    public async Task<Paginator<Model.Academy.StudentEntity>> GetStudentsPaged(PagedRequest pagedRequest)
    {
        var defaultResult = new Paginator<Model.Academy.StudentEntity>();
        var resource = $"{path}{pagedRequest}";
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary, resource, defaultResult);
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
    
    public async Task<bool> UpdateStudentState(string studentId)
    {
        var resource = $"{path}/{studentId}/state";
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, false);
    }
    public async Task<StudentPassDto> GetStudentToken(string studentId)
    {
        var resource = $"{path}/{studentId}/token";
        return  await apiFactory.Default.GetAsync(Modules.Secretary, resource, new StudentPassDto());
    }
    public async Task<bool> UpdateStudentToken(string studentId)
    {
        var resource = $"{path}/{studentId}/token";
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, false);
    }
    
}