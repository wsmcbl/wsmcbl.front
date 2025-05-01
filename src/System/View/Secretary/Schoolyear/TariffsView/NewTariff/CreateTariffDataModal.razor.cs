using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Schoolyear.TariffData;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

public partial class CreateTariffDataModal : ComponentBase
{
    [Inject] protected CreateTariffDataController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    private bool isOverdue { get; set; } = false;
    
    private TariffDataDto Tariff = new();
    private DateOnly dueDate { get; set; }
    private List<DropDownItem>? TariffTypeItemList =
    [
        new() { Id = 1, Name = "Matrícula" },
        new() { Id = 2, Name = "Mensualidad" }
    ];
    private List<DropDownItem> modalityItemList =
    [
        new() { Id = 1, Name = "Preescolar" },
        new() { Id = 2, Name = "Primaria" },
        new() { Id = 3, Name = "Secundaria" }
    ];
    
    private async Task CreateTariffData()
    {

        if (isOverdue)
        {
            Tariff.dueDate = null;
        }
        
        var response = await controller.CreateTariffData(Tariff);
        if (response != null)
        {
            await Notificator!.ShowSuccess("Se ha registrado el arancel correctamente.");
            
        }
        else
        {
            await Notificator!.ShowError( "Hubo un fallo al registrar el arancel.");
        }
    }
}