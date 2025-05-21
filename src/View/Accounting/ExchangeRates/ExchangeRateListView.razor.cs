using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.View.Accounting.ExchangeRates;

public partial class ExchangeRateListView : BaseView
{
    [Inject] private CreateSchoolyearController SchoolYearController { get; set; } = null!;
    [Inject] private ExchangeRateController Controller { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private List<BasicSchoolyearDto> schoolYearList { get; set; } = [];
    private List<ExchangeRateDto>? ExchangeRate { get; set; }
    private ExchangeRateDto EditExchangeRate { get; set; } = new();
    private string EditSchoolYearLabel { get; set; } = "N/A";


    protected override async Task OnParametersSetAsync()
    {
        ExchangeRate = await Controller.GetExchangeRateList();
        schoolYearList = await SchoolYearController.GetSchoolYearList();
    }

    protected override bool IsLoading()
    {
        return ExchangeRate == null;
    }

    private async Task OpenEditModal(ExchangeRateDto exchangeRate, string label)
    {
        EditExchangeRate = exchangeRate;
        EditSchoolYearLabel = label;
        await Navigator.ShowModal("EditExchangeRateModal");
    }
}