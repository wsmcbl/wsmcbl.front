using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class ViewEnrollmentGuideController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly IJSRuntime _jsRuntime;
    
    public ViewEnrollmentGuideController(ApiConsumerFactory apiConsumerFactory, IJSRuntime jsRuntime)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
        _jsRuntime = jsRuntime;
    }
    
    public async Task<EnrollmentDto> GetMyEnrollmentGuide(string teacherId)
    {
        EnrollmentDto defaultResult = new ();
        var result = await _apiConsumer.GetAsync(Modules.Academy, $"teachers/{teacherId}/enrollments/guide", defaultResult);
        result.OrderSubjectList();
        return result;
    }
    
    public async Task GetGradeDocument(string teacherId, int partialId, string EnrollmentLabel)
    {
        var resource = $"teachers/{teacherId}/enrollments/guide/stats/evaluated/export?partialId={partialId}";
        
        var fileBytes = await _apiConsumer.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("No se realizó la descarga del archivo.");
        }
        
        // $$$$$$$$$$$$$$$$$$$$$$$$$$$$$ REPARAR SCHOOLYEAR Y PARTIAL $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        var NameGenerator = new EnrollmentFileNameGenerator(EnrollmentLabel, "2025");
        var fileName = NameGenerator.GetFileName(1, "estadisticas");
        
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }
    
    
}