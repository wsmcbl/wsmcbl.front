using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.GetDateRecordingGrade;
using wsmcbl.src.View.Management.PartialsGradeRecording;

namespace wsmcbl.src.Controller;

public class EnablePartialGradeRecordingController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public EnablePartialGradeRecordingController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<List<PartialDto>> GetPartials()
    {
        List<PartialDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Management, "partials", defaultResult);
    }

    public async Task<bool> ActivePartials(int partialId, bool isActive)
    {
        List<PartialDto> defaultResult = [];
        return await _apiConsumer.PutAsync(Modules.Management, $"partials/{partialId}/activate?isActive={isActive}", defaultResult);
    }

    public async Task<bool> ActiveGradesRecording(int partialId, bool isActive, string endTime)
    { 
        var resource = "";
        resource = isActive == false ? $"partials/{partialId}?enable={isActive}" : $"partials/{partialId}?enable={isActive}&deadline={endTime}";
        List<PartialDto> defaultResult = [];
        return await _apiConsumer.PutAsync(Modules.Management, resource, defaultResult);
    }
    
    public async Task<RecondingGradeDateDto?> GetEnablePartial()
    {
        try
        {
            RecondingGradeDateDto defaultResult = new();
            return await _apiConsumer.GetAsync(Modules.Management, "partials/enables", defaultResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}