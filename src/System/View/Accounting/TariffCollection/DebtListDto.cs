namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DebtListDto
{
    public int tariffId { get; set; }
    public string? schoolYear { get; set; }
    public string? concept { get; set; }
    public double total { get; set; }
    public int discount { get; set; }
    public double arrears { get; set; }
    public double subTotal { get; set; }
    public double debtBalance { get; set; }
    public bool itPaid { get; set; }
}