namespace wsmcbl.src.View.Accounting.TariffCollection;

public class TariffModalDto
{
    public int TariffId { get; set; }
    
    public string Concept { get; set; } = null!;
    
    public double Amount { get; set; }
    
    public double Discount { get; set; }
    
    public double Arrear { get; set; }
    
    public double Total { get;  set; }

    public void computeTotal()
    {
        Total = Amount - Discount + Arrear;
    }
}

