using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Config.UserList;
using wsmcbl.src.View.Components.GetDateRecordingGrade;
namespace wsmcbl.src.Controller;

public class GetDateMaxOfRecordingGradeController
{
    private readonly ApiConsumer _apiConsumer;
    
    public GetDateMaxOfRecordingGradeController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.Default;
    }
    
    public async Task<RecondingGradeDateDto?> GetMaxDate()
    {
        RecondingGradeDateDto defaultResult = new();
        try
        {
            return await _apiConsumer.GetAsync(Modules.Management, "partials/enables", defaultResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}