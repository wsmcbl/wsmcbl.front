using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.ExchangeRates;

public partial class ExchangeRateListView : BaseView
{
    protected override bool IsLoading()
    {
        return false;
    }
}