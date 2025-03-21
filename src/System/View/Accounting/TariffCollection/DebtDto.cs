namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DebtDto
{
    public int tariffId { get; set; }
    public string? schoolyearId { get; set; }
    public string? concept { get; set; }
    public double total { get; set; }
    public int discount { get; set; }
    public double arrears { get; set; }
    public double subTotal { get; set; }
    public double debtBalance { get; set; }
    public bool itPaid { get; set; }
}