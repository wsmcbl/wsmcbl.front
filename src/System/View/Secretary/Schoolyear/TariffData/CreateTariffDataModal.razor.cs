using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffData;

public partial class CreateTariffDataModal : ComponentBase
{
    [Parameter] public List<DropDownItem>? TariffTypeItemList { get; set; }
    [Inject] protected CreateSchoolyearController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    
    private SchoolyearTariffDto SelectedSchoolyearTariff = new();
    private List<DropDownItem> modalityItemList =
    [
        new() { Id = 1, Name = "Preescolar" },
        new() { Id = 2, Name = "Primaria" },
        new() { Id = 3, Name = "Secundaria" }
    ];
    
    private async Task SaveNewTariff(SchoolyearTariffDto schoolyearTariff)
    {
        var response = await controller!.CreateNewTariff(schoolyearTariff);
        if (response)
        {
            await Notificator!.ShowSuccess("Se ha registrado el arancel correctamente.");
        }
        else
        {
            await Notificator!.ShowError( "Hubo un fallo al registrar el arancel.");
        }
    }
}