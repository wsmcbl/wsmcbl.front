using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Accounting.ExchangeRates;

public partial class ExchangeRateEditModal : ComponentBase
{
    [Inject] private ExchangeRateController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Parameter] public ExchangeRateDto ExchangeRate { get; set; } = new();
    [Parameter] public EventCallback OnUpdate { get; set; }
    [Parameter] public string SchoolYearLabel {get; set;} = string.Empty; 
    
    private async Task UpdateExchangeRate()
    {
       var response = await Controller.UpdateExchangeRate(ExchangeRate.value);
       if (response)
       {
           await Notificator.ShowSuccess("Se ha actualizado el valor del tipo de cambio correctamente.");
           await OnUpdate.InvokeAsync();
           await Navigator.HideModal("EditExchangeRateModal");
           return;
       }

       await Notificator.ShowError("No hemos podido actualizar el tipo de cambio, intentelo mas tarde.");
    }
}