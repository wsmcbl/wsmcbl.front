using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Management.Register;

namespace wsmcbl.src.Controller;

public class GenerateStudentRegisterController : BaseController
{
    private readonly IJSRuntime _jsRuntime;

    public GenerateStudentRegisterController(ApiConsumerFactory apiFactory, IJSRuntime jsRuntime): base(apiFactory, "students/registers")
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<Paginator<RegisterDto>> GetStudentRegisterPaged(PagedRequest pagedRequest)
    {
        Paginator<RegisterDto> defaultResult = new ();
        
        return await apiFactory
            .WithNotificator.GetAsync(Modules.Secretary,$"{path}{pagedRequest}", defaultResult);
    }

    public async Task GetCurrentStudentRegister()
    {
        var fileBytes = await apiFactory.WithNotificator.GetBackupAsync(Modules.Secretary, $"{path}/documents");
        
        if (fileBytes.Length > 0)
        {
            var fileName = $"Padron.{GetFormattedDate()}.xlsx";
            var base64 = Convert.ToBase64String(fileBytes);
            var url = $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64}";
            await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
        }
    }
    
    private string GetFormattedDate()
    {
        var timeZoneUTC6 = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
        var value = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneUTC6);
        return value.ToString("ddMMyy.HHmm");
    }
}