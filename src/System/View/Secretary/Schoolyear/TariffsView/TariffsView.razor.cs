using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear.TariffData;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView;

public partial class TariffsView : BaseView
{
    [Inject] CreateSchoolyearController Controller { get; set; } = null!;
    [Inject] Navigator Navigator { get; set; } = null!;
    private List<TariffDataDto>? tariffList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        tariffList = await Controller.GetTariffList();
    }
    protected override bool IsLoading()
    {
        return tariffList == null;
    }

    private async Task OpenNewTariffModal()
    {
        await Navigator.ShowModal("ModalNewTariff");
    }
}