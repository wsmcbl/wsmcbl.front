namespace wsmcbl.front.Models.Accounting;


public class Transaction
{
    public string TransactionId { get; set; }
    public string CashierId { get; set; }
    public string StudentId { get; set; }
    public double Total { get; set; }
    public double Discount { get; set; }
    public DateTime DateTime { get; set; }
    public List<Tariff> Tariffs { get; set; }
}