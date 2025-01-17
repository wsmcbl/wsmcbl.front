using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class ModalNewTariff : ComponentBase
{
    [Parameter] public List<DropdownList>? DropdownTypeTariffsLists { get; set; }
    [Inject] protected CreateSchoolYearController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    
    private SchoolyearTariffDto SelectedSchoolyearTariff = new();
    private List<DropdownList> DropdownModalityLists =
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
            await Notificator!.ShowSuccess("Éxito", "Arancel guardado correctamente");
        }
        else
        {
            await Notificator!.ShowError("Error", "No pudimos guardar el arancel");
        }
    }
}