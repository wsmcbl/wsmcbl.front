using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

public partial class TariffsView : BaseView
{
    [Inject] private CreateSchoolyearController Controller { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private List<CreateTariffDto>? tariffList { get; set; }
    private CreateTariffDto EditTariff { get; set; } = new();

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

    private async Task OpenEditTariffModal(CreateTariffDto item)
    {
        EditTariff = item;
         await Navigator.ShowModal("ModalEditTariff");
    }
}