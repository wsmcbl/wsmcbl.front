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
    
    public async Task<EnrollmentEntity?> CreateEnrollments(string degreeId, int quantity, EnrollmentEntity Default)
    {
        var route = $"degrees/{degreeId}/enrollments?quantity={quantity}";
        return await _apiConsumer.PostAsync<object, EnrollmentEntity>(Modules.Secretary, route, null, Default);
    }
    
    public async Task<List<DegreeEntity>> GetDegreeList()
    {
        List<DegreeEntity> Default = [];
        return await _apiConsumer.GetAsync(Modules.Secretary, "degrees", Default);
    }    
}