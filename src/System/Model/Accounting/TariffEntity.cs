using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.Model.Accounting;

public class TariffEntity
{
    public int tariffId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public decimal Discount { get; set; }
    public decimal Arrears { get; set; }
    public decimal SubAmount { get;  set; }
    public decimal Total { get;  set; }
    public DateOnlyDto? DueDate { get; set; }
    public bool isLate { get; set; }
    public int type { get; set; }
    public int educationalLevel { get; set; }
    
    public void ComputeTotal()
    {
        Total = SubAmount - Discount + Arrears;
    }

    private const decimal ARREARS_RATE = 0.1m; 
    public void SetSubamount(decimal discountRate)
    {
        SubAmount = amount;
        Discount = type == 1 ? amount*discountRate : 0;
        Arrears = isLate ? amount*(1 - discountRate)*ARREARS_RATE : 0;
    }

    public string GetStringIsLate() => isLate ? "Sí" : "No";
}
