namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DebtDto
{
    public int tariffId { get; set; }
    public string? schoolyearId { get; set; }
    public string? concept { get; set; }
    public decimal total { get; set; }
    public decimal discount { get; set; }
    public decimal arrears { get; set; }
    public decimal subTotal { get; set; }
    public decimal debtBalance { get; set; }
    public bool itPaid { get; set; }

    public DebtDto()
    {
        
    }
}