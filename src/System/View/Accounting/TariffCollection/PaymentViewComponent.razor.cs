using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public partial class PaymentViewComponent : ComponentBase
{
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

        foreach (var item in TariffList)
        {
            Subtotal += item.Total;
            Discount += item.Discount;
            Arrears += item.Arrears;
        }

        Total = Subtotal;
    }

    protected async Task MakePayment()
    {
        //Controller.BuildTransaction(TariffsToPay!, AreArrearsApply);
        TariffList = [];
    }
    
    
    protected void ExonerateArrears(ChangeEventArgs e)
    {
        AreArrearsApply = (bool)e.Value!;

        Total += (AreArrearsApply ? -1 : 1) * Arrears;
    }
}