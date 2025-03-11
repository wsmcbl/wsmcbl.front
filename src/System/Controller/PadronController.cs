using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Management.Padron;

namespace wsmcbl.src.Controller;

public class PadronController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly IJSRuntime _jsRuntime;

    public PadronController(ApiConsumerFactory apiConsumerFactory, IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }

    public async Task<Paginator<PadronDto>> GetPadronList(PagedRequest pagedRequest)
    {
        Paginator<PadronDto> defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Secretary,$"students/registers{pagedRequest}", defaultResult);
    }

    public async Task DownloadPadron()
    {
        var fileBytes = await _apiConsumer.GetBackupAsync(Modules.Secretary, "students/registers/documents");
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
        return DateTime.Now.ToString("ddMMyyyy.HHmm");
    }
}