namespace wsmcbl.src.View.Accounting.TariffCollection;

public class PaymentDto
{
    public int tariffId { get; set; }
    public string concept { get; set; } = null!;
    public double amount { get; set; }
    public double discount { get; set; }
    public double arrears { get; set; } 
    public double debtBalance { get; set; }
}

