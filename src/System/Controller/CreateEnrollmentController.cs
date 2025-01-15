using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.Controller;

public class CreateEnrollmentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public CreateEnrollmentController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
    }
    
    public async Task<EnrollmentEntity?> CreateEnrollments(string degreeId, int quantity)
    {
        var route = $"degrees/{degreeId}/enrollments?quantity={quantity}";
        
        EnrollmentEntity defaultValue = new();
        var result = await _apiConsumer.PostAsync<object, EnrollmentEntity>(Modules.Secretary, route, null, defaultValue);
        
        return result != defaultValue ? result : null;
    }
    
    public async Task<List<DegreeEntity>> GetDegreeList()
    {
        List<DegreeEntity> Default = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, "degrees", Default);
    }    
}