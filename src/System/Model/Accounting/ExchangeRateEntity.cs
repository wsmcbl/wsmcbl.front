namespace wsmcbl.src.Model.Accounting;

public class ExchangeRateEntity
{
    public int rateId { get; set; }
    public string schoolyearId { get; set; } = null!;
    public decimal value { get; set; }
}