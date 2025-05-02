namespace wsmcbl.src.Model.Accounting;

public class ExchangeRateEntity
{
    public int rateId { get; set; }
    public string schoolyearId { get; set; } = "N/A";
    public decimal value { get; set; }
}