using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.Controller;

public class CreateEnrollmentController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public CreateEnrollmentController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<DegreeEntity?> CreateEnrollments(string degreeId, int quantity)
    {
        var route = $"degrees/{degreeId}/enrollments?quantity={quantity}";
        
        DegreeEntity defaultValue = new();
        var result = await _apiConsumer.PostAsync<object, DegreeEntity>(Modules.Secretary, route, null, defaultValue);
        
        return result != defaultValue ? result : null;
    }
    
    public async Task<Paginator<DegreeEntity>> GetDegreeList(PagedRequest pagedRequest)
    {
        return await _apiConsumer.GetAsync(Modules.Secretary, $"degrees{pagedRequest}", new Paginator<DegreeEntity>());
    }    
    
    public async Task<bool> InitializerEnrollment(SaveInitializerDto dto)
    {
        return await _apiConsumer.PutAsync(Modules.Secretary, "degrees/enrollments", dto);
    }   
}