using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Academy.EnrollmentGuide;

namespace wsmcbl.src.Controller;

public class ViewEnrollmentGuideController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly IJSRuntime _jsRuntime;
    private readonly Notificator _notificator;
    private readonly GetSchoolYearServices _schoolYearServices;

    
    public ViewEnrollmentGuideController(ApiConsumerFactory apiConsumerFactory, IJSRuntime jsRuntime, Notificator notificator, GetSchoolYearServices schoolYearServices)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
        _jsRuntime = jsRuntime;
        _notificator = notificator;
        _schoolYearServices = schoolYearServices;
    }
    
    public async Task<EnrollmentDto> GetMyEnrollmentGuide(string teacherId)
    {
        EnrollmentDto defaultResult = new ();
        var result = await _apiConsumer.GetAsync(Modules.Academy, $"teachers/{teacherId}/enrollments/guide", defaultResult);
        result.OrderSubjectList();
        return result;
    }
    
    public async Task GetStatisticsDocument(string teacherId, int partialId,string partialLabel, string enrollmentLabel)
    {
        var resource = $"teachers/{teacherId}/enrollments/guide/stats/evaluated/export?partialId={partialId}";
        
        var fileBytes = await _apiConsumer.GetByteFileAsync(Modules.Academy, resource);
        if (fileBytes.Length <= 0)
        {
            await _notificator.ShowError("No se realizó la descarga del archivo. \n\n Es posible que no existan registros para el parcial seleccionado.");
            return;
        }
        
        var schoolYearLabel = await _schoolYearServices.GetSchoolYearActiveLabel();
        var nameGenerator = new EnrollmentFileNameGenerator(enrollmentLabel, schoolYearLabel);
        var fileName = nameGenerator.GetFileName(partialLabel, "estadisticas");
        
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
        await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
    }
    
    
}