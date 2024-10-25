using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class ModalEditTariff : ComponentBase
{
    [Parameter]
    public List<DropdownList> DropdownTypeTariffsLists { get; set; }
    [Parameter]
    public EventCallback OnEditCompleted { get; set; }
    
    
    [Inject] protected Navigator navigator { get; set; }
    private SchoolYearTariffs SelectedTariff = new();
    
    
    protected List<DropdownList> DropdownModalityLists =
    [
        new DropdownList { Id = 1, Nombre = "Preescolar" },
        new DropdownList { Id = 2, Nombre = "Primaria" },
        new DropdownList { Id = 3, Nombre = "Secundaria" }
    ];
    
    public async Task EditTariff(SchoolYearTariffs item)
    {
        SelectedTariff = item;
        SelectedTariff.OnlyDate = DateOnly.FromDateTime(DateTime.Now);
        await navigator.ShowModal("ModalEditTariff");
    }
    
    public async Task CompleteEdit()
    {
        await navigator.HideModal("ModalEditTariff");
        await OnEditCompleted.InvokeAsync();
    }
    
    
}