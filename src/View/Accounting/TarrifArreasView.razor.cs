using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.View.Accounting;

public class TariffArrears : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected Notificator? AlertService { get; set; }
    
    private int ApplyArreas { get; set; }
    private List<TariffDto> SelectedTariffs { get; set; } = [];
    protected ICollection<TariffDto> Tariffs { get; set; } = null!;

    protected override async Task OnParametersSetAsync()
    {
        Tariffs = await Controller.GetTariffsOverdue("state:overdue");
    }

    protected void OnSelectItemChanged(ChangeEventArgs e, TariffDto tariff)
    {
        if ((bool)e.Value)
        {
            if (!SelectedTariffs.Contains(tariff))
            {
                SelectedTariffs.Add(tariff);
                ApplyArreas = tariff.TariffId;
            }
        }
        else
        {
            if (SelectedTariffs.Contains(tariff))
            {
                SelectedTariffs.Remove(tariff);
            }
        }
    }

    protected async Task Confirm()
    {
        var title = "¿Seguro que desea generar mora?";
        var content = "La mora se aplicará a todo estudiante con mensualidad atrasada.";
        var options = ("Aplicar.", "No guardar.");

        if (!await AlertService.AlertQuestionTwoOptions(title, content, options))
        {
            await AlertService.AlertInformation("Mora no aplicada.", string.Empty);
            return;
        }
        
        var response = await Controller.ActiveArrears(ApplyArreas);

        if (response)
        {
            await AlertService.AlertSuccess("¡Mora generada correctamente!", string.Empty);
        }
        else
        {
            await AlertService.AlertInformation("Obtuvimos problemas al generar la mora.", string.Empty);
        }
    }
}