using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class ModalNewTariff : ComponentBase
{
    [Parameter]
    public List<DropdownList> DropdownTypeTariffsLists { get; set; }
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; }
    [Inject] protected Notificator Notificator { get; set; }
    
    protected SchoolyearTariffDto SelectedSchoolyearTariff = new();
    protected List<DropdownList> DropdownModalityLists =
    [
        new DropdownList { Id = 1, Name = "Preescolar" },
        new DropdownList { Id = 2, Name = "Primaria" },
        new DropdownList { Id = 3, Name = "Secundaria" }
    ];
    
    protected async Task SaveNewTariff(SchoolyearTariffDto schoolyearTariff)
    {
        var response = await Controller.CreateNewTariff(schoolyearTariff);
        if (response)
        {
            await Notificator.ShowSuccess("Éxito", "Arancel guardado correctamente");
        }
        else
        {
            await Notificator.ShowError("Error", "No pudimos guardar el arancel");
        }
    }
    
    
}