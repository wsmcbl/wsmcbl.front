using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.Controller;

public class CreateBackupController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly IJSRuntime _jsRuntime;

    public CreateBackupController(ApiConsumerFactory apiConsumerFactory, IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _apiConsumer = apiConsumerFactory.WithNotificator;
    }

    
    public async Task DownloadBackup()
    {
        var fileBytes = await _apiConsumer.GetBackupAsync(Modules.Config, "backups/current");
        if (fileBytes.Length > 0)
        {
            var fileName = $"WSM_Backup_{GetFormattedDate()}.sql";
            var base64 = Convert.ToBase64String(fileBytes);
            var url = $"data:application/sql;base64,{base64}";
            
            await _jsRuntime.InvokeVoidAsync("downloadFile", fileName, url);
        }
    }

    private string GetFormattedDate()
    {
        return DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
}