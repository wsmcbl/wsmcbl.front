using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.Model.Accounting;

public class TariffEntity
{
    public int tariffId { get; set; }
    public string schoolyearId { get; set; } = "N/A";
    public string concept { get; set; } = "N/A";
    public decimal amount { get; set; }
    public decimal Discount { get; set; }
    public decimal Arrears { get; set; }
    public decimal SubAmount { get;  set; }
    public decimal Total { get;  set; }
    public DateOnlyDto? dueDate { get; set; }
    public bool isLate { get; set; }
    public int typeId { get; set; }
    public int educationalLevel { get; set; }
    
    public void ComputeTotal()
    {
        Total = SubAmount - Discount + Arrears;
    }

    private const decimal ARREARS_RATE = 0.1m; 
    public void SetSubamount(decimal discountRate)
    {
        SubAmount = amount;
        Discount = typeId == 1 ? amount*discountRate : 0;
        Arrears = isLate ? amount*(1 - discountRate)*ARREARS_RATE : 0;
    }

    public string GetStringIsLate() => isLate ? "Sí" : "No";
}
