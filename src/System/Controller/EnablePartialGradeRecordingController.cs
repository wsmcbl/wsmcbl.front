using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Management.PartialsGradeRecording;

namespace wsmcbl.src.Controller;

public class EnablePartialGradeRecordingController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;

    public EnablePartialGradeRecordingController(ApiConsumerFactory apiConsumerFactory)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }
    
    public async Task<List<PartialListDto>> GetPartials()
    {
        List<PartialListDto> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Management, "partials", defaultResult);
    }

    public async Task<bool> ActivePartials(int partialId, bool isActive)
    {
        List<PartialListDto> defaultResult = [];
        return await _apiConsumer.PutAsync(Modules.Management, $"partials/{partialId}/activate?isActive={isActive}", defaultResult);
    }

    public async Task<bool> ActiveGradesRecording(int partialId, bool isActive, DateTime endTime)
    { 
        var resource = "";
        resource = isActive == false ? $"partials/{partialId}?enable={isActive}" : $"partials/{partialId}?enable={isActive}&deadline={endTime}";
        List<PartialListDto> defaultResult = [];
        return await _apiConsumer.PutAsync(Modules.Management, resource, defaultResult);
    }
}