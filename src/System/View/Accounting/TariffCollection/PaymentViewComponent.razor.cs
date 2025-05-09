using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public partial class PaymentViewComponent : ComponentBase
{
    [Inject] protected Navigator Navigator { get; private set; } = null!;
    [Parameter] public List<TariffEntity> TariffList { get; set; } = null!;

    private decimal Arrears { get; set; }
    private decimal Subtotal { get; set; }
    private decimal Discount { get; set; }
    private decimal Total { get; set; }

    private decimal AmountToDivide { get; set; }

    private bool ExonerateArrears;
    private bool ExonerateArrearsChecked
    {
        get => ExonerateArrears;
        set
        {
            ExonerateArrears = value;
            Total += (ExonerateArrears ? -1 : 1) * Arrears;
            Total = Math.Round(Total, 2);
        }
    }


    protected override void OnParametersSet()
    {
        ComputeTotal();
        AmountToDivide = 0;
    }

    private void ComputeTotal()
    {
        Subtotal = 0;
        Discount = 0;
        Arrears = 0;
        Total = 0;

        foreach (var item in TariffList)
        {
            Subtotal += item.amount;
            Discount += item.Discount;
            Arrears += item.Arrears;
            Total += item.Total;
        }

        Total = Math.Round(Total, 2);
    }

    [Parameter] public EventCallback<List<DetailDto>> CollectTariff { get; set; }

    private async Task CreateDetail(decimal amountToPay)
    {
        var detail = TariffList.MapToDto(!ExonerateArrears, amountToPay);

        await CollectTariff.InvokeAsync(detail);
        TariffList = [];
        await Navigator.HideModal("PaymentView");
    }

    private void HandleInputChange(ChangeEventArgs e)
    {
        AmountToDivide = string.IsNullOrEmpty(e.Value?.ToString()) ? 0 : Convert.ToDecimal(e.Value);
        StateHasChanged();
    }

    private async Task HandleKeyDown(KeyboardEventArgs key)
    {
        if (AmountToDivide > 0 && key is { Key: "Enter", CtrlKey: true })
        {
            await CreateDetail(AmountToDivide);
        }
    }
}