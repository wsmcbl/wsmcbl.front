using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.View.Components;

public partial class ForgetDeb : ComponentBase
{
    [Parameter] public string StudentId { get; set; } = null!;
    [Parameter] public int TariffId { get; set; }
    [Inject] public Notificator Notificator { get; set; } = null!;
    [Inject] public CollectTariffController Controller { get; set; } = null!;
    [Inject] public Navigator Navigator { get; set; } = null!;
    [Parameter] public EventCallback FinishTask { get; set; }
    private string AuthToken { get; set; } = null!;


    private async Task DebitTariff()
    {
        var result = await Notificator.ShowAlertQuestion("Alerta", "¿Está seguro de debitar esta tarifa?",
            ("Sí","No"));
        if (!result)
        {
            return;
        }
        
        var dto = new DebDto(StudentId, TariffId, AuthToken);
        var response = await Controller.DebitTariff(dto);
        if (!response)
        {
            await Notificator.ShowError("Hubo un fallo al debitar la tarifa.");
            return;
        }
        
        await Notificator.ShowSuccess("Se ha debitado la tarifa correctamente.");
        await Navigator.HideModal("ForgetDebtModal");
        await FinishTask.InvokeAsync();
    }
}