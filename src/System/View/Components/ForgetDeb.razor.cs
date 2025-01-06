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
        var dto = new DebDto(StudentId, TariffId, AuthToken);
        var request = await Notificator.ShowAlertQuestion("Alerta", "¿Estas seguro que deseas debitar esta tarifa?",("Si","No"));
        if (request)
        {
            var response = await Controller.DebitTariff(dto);
            if (response)
            {
                await Notificator.ShowSuccess("Exito","Hemos debitados exitosamente la tarifa");
                await Navigator.HideModal("ForgetDebtModal");
                await FinishTask.InvokeAsync();
                return;
            }
            await Notificator.ShowError("Error", "No tubimos exito al debitar");
        }
    }
}