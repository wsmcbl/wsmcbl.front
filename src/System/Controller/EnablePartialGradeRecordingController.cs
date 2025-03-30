using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Components.GetDateRecordingGrade;
using wsmcbl.src.View.Management.PartialsGradeRecording;

namespace wsmcbl.src.Controller;

public class EnablePartialGradeRecordingController : BaseController
{
    public EnablePartialGradeRecordingController(ApiConsumerFactory apiFactory) : base(apiFactory, "partials")
    {
    }
    
    public async Task<List<PartialDto>> GetPartials()
    {
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Management, path, new List<PartialDto>());
    }

    public async Task<bool> ActivePartials(int partialId, bool isActive)
    {
        var resource = $"{path}/{partialId}/activate?isActive={isActive}";
        
        return await apiFactory
            .WithNotificator.PutAsync(Modules.Management, resource, new List<PartialDto>());
    }

    public async Task<bool> ActiveGradesRecording(int partialId, bool isActive, string endTime)
    { 
        var resource = $"partials/{partialId}?enable={isActive}";

        if (isActive)
        {
            resource = $"{resource}&deadline={endTime}";
        }
        
        return await apiFactory.WithNotificator.PutAsync(Modules.Management, resource, new List<PartialDto>());
    }
    
    public async Task<RecondingGradeDateDto?> GetEnablePartial()
    {
        try
        {
            RecondingGradeDateDto defaultResult = new();
            
            return await apiFactory
                .Default.GetAsync(Modules.Management, $"{path}/enables", defaultResult);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}