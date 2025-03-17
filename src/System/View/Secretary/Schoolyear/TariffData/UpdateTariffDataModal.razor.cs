using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffData;

public partial class UpdateTariffDataModal : ComponentBase
{
    [Parameter] public List<DropDownItem> TariffTypeItemList { get; set; } = null!;
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    
    
    [Inject] protected Navigator? navigator { get; set; }
    private TariffDataDto? SelectedTariff { get; set; }
    
    protected override void OnParametersSet()
    {
        SelectedTariff = new TariffDataDto();
    }

    public async Task EditTariff(TariffDataDto item)
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