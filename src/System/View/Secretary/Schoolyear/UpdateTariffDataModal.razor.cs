using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class UpdateTariffDataModal : ComponentBase
{
    [Parameter] public List<DropDownItem> TariffTypeItemList { get; set; } = null!;
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    
    
    [Inject] protected Navigator? navigator { get; set; }
    protected TariffAuxEntity? SelectedTariff { get; set; }
    
    protected override void OnParametersSet()
    {
        SelectedTariff = new TariffAuxEntity();
    }

    public async Task EditTariff(TariffAuxEntity item)
    {
        SelectedTariff = item;
        await navigator!.ShowModal("ModalEditTariff");
    }
    
    public async Task CompleteEdit()
    {
        await navigator!.HideModal("ModalEditTariff");
        await OnEditCompleted.InvokeAsync();
    }
    
    
}