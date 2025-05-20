using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.View.Accounting.ExchangeRates;

public partial class ExchangeRateListView : BaseView
{
    [Inject] private CreateSchoolyearController SchoolYearController { get; set; } = null!;
    [Inject] private ExchangeRateController Controller { get; set; } = null!;
    private List<BasicSchoolyearDto> schoolYearList { get; set; } = [];
    private List<ExchangeRateDto>? ExchangeRate { get; set; }


    protected override async Task OnParametersSetAsync()
    {
        ExchangeRate = await Controller.GetExchangeRateList();
        schoolYearList = await SchoolYearController.GetSchoolYearList();
    }

    protected override bool IsLoading()
    {
        return ExchangeRate == null;
    }
}