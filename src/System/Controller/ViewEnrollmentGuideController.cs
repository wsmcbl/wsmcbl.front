using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class ViewEnrollmentGuideController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public ViewEnrollmentGuideController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<EnrollmentDto> GetMyEnrollmentGuide(string teacherId)
    {
        EnrollmentDto defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Academy, $"teachers/{teacherId}/enrollments/guide", defaultResult);
    }
    
}