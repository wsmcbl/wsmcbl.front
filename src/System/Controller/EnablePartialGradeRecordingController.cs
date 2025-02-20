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
        return  await _apiConsumer.GetAsync(Modules.Management, "partials", defaultResult);
    }
}