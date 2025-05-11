namespace wsmcbl.src.View.Accounting.TariffCollection;

public class PaymentDto
{
    public int tariffId { get; set; }
    public string schoolYear { get; set; } = null!;
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public decimal discount { get; set; }
    public decimal arrears { get; set; } 
    public decimal subTotal { get; set; } 
    public decimal debtBalance { get; set; }
    public bool itPaidLate { get; set; }
}

