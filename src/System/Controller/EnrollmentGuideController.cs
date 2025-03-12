using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class EnrollmentGuideController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public EnrollmentGuideController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<EnrollmentDto> GetMyEnrollmentGuide(string teacherId)
    {
        EnrollmentDto defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Academy, $"teachers/{teacherId}/enrollments/guide", defaultResult);
    }
    
}