using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config.UserList;
using wsmcbl.src.View.Components.GetDateRecordingGrade;
namespace wsmcbl.src.Controller;

public class GetDateMaxOfRecordingGradeController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    
    public GetDateMaxOfRecordingGradeController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<RecondingGradeDateDto?> GetMaxDate()
    {
        RecondingGradeDateDto defaultResult = new();
        return await _apiConsumer.GetAsync(Modules.Management, "partials/enables", defaultResult);
    }
}