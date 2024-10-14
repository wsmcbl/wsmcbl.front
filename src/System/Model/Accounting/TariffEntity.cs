namespace wsmcbl.src.Model.Accounting;

public class TariffEntity
{
    public int TariffId { get; set; }
    public string SchoolYear { get; set; } = null!;
    public string Concept { get; set; } = null!;
    public double Amount { get; set; }
    public double Discount { get; set; }
    public double Arrears { get; set; }
    public double SubTotal { get;  set; }
    public double Total { get;  set; }
    public string? DueDate { get; set; }
    public bool IsLate { get; set; }
    public int Type { get; set; }
    
    public void ComputeTotal()
    {
        Total = Amount - Discount + Arrears;
    }

    private const double ARREARS_RATE = 0.1; 
    public void UpdateDiscount(double discount)
    {
        Discount = Type == 1 ? Amount*discount : 0;
        Arrears = IsLate ? Amount*(1 - discount)*ARREARS_RATE : 0;
    }
}
