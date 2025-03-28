using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffData;

public partial class CreateTariffDataModal : ComponentBase
{
    [Inject] protected CreateTariffDataController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    
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
    
    private async Task CreateTariffData(TariffDataDto tariff)
    {
        var response = await controller.CreateTariffData(tariff);
        if (response == null)
        {
            await Notificator!.ShowSuccess("Se ha registrado el arancel correctamente.");
        }
        else
        {
            await Notificator!.ShowError( "Hubo un fallo al registrar el arancel.");
        }
    }
}