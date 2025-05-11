using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Config.Backup;

public partial class BackupView : ComponentBase
{
    [Inject] CreateBackupController Controller { get; set; } = default!;
    
    private async Task GetBackup()
    {
       await Controller.DownloadBackup();
    }
}