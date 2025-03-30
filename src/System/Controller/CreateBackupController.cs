using Microsoft.JSInterop;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Utilities;

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
        const string resource = "backups/current/export";
        
        var fileBytes = await _apiConsumer.GetByteFileAsync(Modules.Config, resource);
        if (fileBytes.Length <= 0)
        {
            throw new InternalException("Ocurrió un error al descargar el archivo.");
        }
        
        var base64 = Convert.ToBase64String(fileBytes);
        var url = $"data:application/sql;base64,{base64}";
            
        await _jsRuntime.InvokeVoidAsync("downloadFile", GetFileName(), url);
    }

    private static string GetFileName()
    {
        return $"wsm.backup.{DateTime.Now:ddMMyy.HHmm}.sql";
    }
}