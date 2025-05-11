using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.ApplyArrears;

public partial class TariffOverdueListView : BaseView
{
    [Inject] private ApplyArrearsController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private GetSchoolYearServices GetSchoolYearServices { get; set; } = null!;
    private List<TariffEntity>? Tariffs { get; set; }
    private Dictionary<string, string>? SchoolYearLabels { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        await LoadTariffList();
    }
    private async Task LoadTariffList()
    {
        Tariffs = await Controller.GetTariffOverdueList();
        var schoolYearId = Tariffs.Select(s => s.schoolyearId).Distinct().ToList();
        SchoolYearLabels = await GetSchoolYearServices.GetSchoolYearLabelsBatch(schoolYearId);
    }
    private async Task ActiveArrears(int tariffId)
    {
        var active = await Notificator.ShowAlertQuestion("Advertencia",
            "¿Esta seguro que desea activar la mora?", ("Activar","Cancelar"));
        if (!active)
        {
            return;
        }
        
        var response = await Controller.ActiveArrears(tariffId);
        if (!response)
        {
            await Notificator.ShowError("Error", "No hemos podido activar la mora, intentelo mas tarde");
            return;
        }

        await LoadTariffList();
        await Notificator.ShowSuccess("Exito", "Hemos activado la mora correctamente.");
    }
    protected override bool IsLoading()
    {
        return Tariffs != null;
    }
}