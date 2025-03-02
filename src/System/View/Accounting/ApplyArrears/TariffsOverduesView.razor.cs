using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.ApplyArrears;

public partial class TariffsOverduesView : BaseView
{
    [Inject] ApplyArrearsController Controller { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;
    protected List<DropDownItem> TariffTypeItemList { get; set; } = new();
    private List<TariffEntity> Tariffs { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        Tariffs = await Controller.GetTariffsOverdues();
        TariffTypeItemList = await Controller.GetTypeTariffList();
    }

    private async Task ActiveArrears(int tariffId)
    {
        var active = await Notificator.ShowAlertQuestion("Advertencia", "¿Esta seguro que desea activar la mora?",( "Activar","Cancelar"));
        if (active)
        {
            var response = await Controller.ActiveArrears(tariffId);
            if (response)
            {
                await Notificator.ShowSuccess("Exito", "Hemos activado la mora correctamente.");
                return;
            }
        
            await Notificator.ShowError("Error", "No hemos podido activar la mora, intentelo mas tarde");
        }
    }
    
    protected override bool IsLoading()
    {
        return Tariffs.Any();
    }
    
    private string GetStatusLabel(bool value) => value ? "active-status" : "inactive-status";
}