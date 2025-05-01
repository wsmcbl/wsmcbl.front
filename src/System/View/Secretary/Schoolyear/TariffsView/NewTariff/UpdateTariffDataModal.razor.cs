using Microsoft.AspNetCore.Components;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Schoolyear.TariffData;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

public partial class UpdateTariffDataModal : ComponentBase
{
    [Parameter] public List<DropDownItem> TariffTypeItemList { get; set; } = null!;
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    
    
    [Inject] protected Navigator? navigator { get; set; }
    private TariffDataDto? SelectedTariff { get; set; }
    
    private DateOnly dueDate { get; set; }
    
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