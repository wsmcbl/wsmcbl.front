using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.UnenrollmentsView;

namespace wsmcbl.src.Controller;

public class UnenrollmentController : BaseController
{
    public UnenrollmentController(ApiConsumerFactory apiFactory) : base(apiFactory, "withdrawns/students")
    {
    }
    
    public async Task<List<UnenrollmentBasicDto>> GetStudents()
    {
        List<UnenrollmentBasicDto> defaultResult = [];
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary, path, defaultResult);
    }

    public async Task<bool> Withdraw(string studentId)
    {
        var resource = $"{path}/{studentId}";
        return await apiFactory.WithNotificator.PutAsync(Modules.Secretary, resource, studentId);
    }
}