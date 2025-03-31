using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Pages.HomePages;

public partial class Cashier : ComponentBase
{
    [Inject] GenerateDebtorReportController Controller { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;
    private byte[]? Pdf { get; set; } = [];

    
    private async Task GetPdfContent()
    { 
        Pdf = await Controller.GetDebtorReport();
        if (Pdf.Length == 0)
        {
            await Notificator.ShowError("No se pudo generar el documento.");
            return;
        }
        
        await Navigator.ShowPdfModal();
    }
    
}