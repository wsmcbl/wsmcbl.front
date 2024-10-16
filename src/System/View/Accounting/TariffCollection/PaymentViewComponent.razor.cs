using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public partial class PaymentViewComponent : ComponentBase
{
    [Inject] protected Navigator Navigator { get; private set; } = null!;
    [Parameter] public List<TariffEntity> TariffList { get; set; } = null!;
    
    protected double Arrears { get; set; }
    protected double Subtotal { get; set; }
    protected double Discount { get; set; }
    protected double Total { get; set; }

    protected double AmountToDivide { get; set; }
    private bool AreArrearsApply { get; set; } = true;


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
            Subtotal += item.Amount;
            Discount += item.Discount;
            Arrears += item.Arrears;
            Total += item.Total;
        }
    }
    
    [Parameter] public EventCallback<List<DetailDto>> OnComplexObjectCreated { get; set; }

    private async Task CreateDetail()
    {
        var detail = TariffList.MapToDto(AreArrearsApply);
        await OnComplexObjectCreated.InvokeAsync(detail);
        TariffList = [];
        await Navigator.HideModal("PaymentView");
    }
    
    
    protected void ExonerateArrears(ChangeEventArgs e)
    {
        AreArrearsApply = (bool)e.Value!;

        Total += (AreArrearsApply ? -1 : 1) * Arrears;
    }
}