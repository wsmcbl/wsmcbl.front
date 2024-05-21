namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;


public class Transaction
{
    [JsonProperty("transactionId")]
    public string TransactionId { get; set; } = null!;

    [JsonProperty("cashierId")]
    public string CashierId { get; set; } = null!;

    [JsonProperty("studentId")]
    public string StudentId { get; set; } = null!;

    [JsonProperty("total")]
    public double Total { get; set; }

    [JsonProperty("discount")]
    public double Discount { get; set; }

    [JsonProperty("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonProperty("tariffs")]
    public List<Tariff> Tariffs { get; set; } = null!;
}

public class TransactionDto
{
    public string cashierId { get; set; } = null!;
    public string studentId { get; set; } = null!;
    public double discount { get; set; }
    public DateTime dateTime { get; set; }
    public List<Tariff> tariffs { get; set; } = null!;
}


public class InvoiceDto
{
    [JsonProperty("transactionId")]
    public string TransactionId { get; set; } = null!;

    [JsonProperty("cashierName")]
    public string CashierName { get; set; } = null!;

    [JsonProperty("studentId")]
    public string StudentId { get; set; } = null!;

    [JsonProperty("studentName")]
    public string StudentName { get; set; } = null!;

    [JsonProperty("total")]
    public double Total { get; set; }

    [JsonProperty("subtotal")]
    public double Subtotal { get; set; }

    [JsonProperty("discount")]
    public double Discount { get; set; }

    [JsonProperty("areals")]
    public double Areals { get; set; }

    [JsonProperty("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonProperty("tariffs")]
    public List<Tariff> Tariffs { get; set; } = null!;

    public InvoiceDto(string transactionId, string cashierName, string studentId, 
    string studentName, double total, double discount, double areals, DateTime dateTime, List<Tariff> tariffs)
    {
        TransactionId = transactionId;
        CashierName = cashierName;
        StudentId = studentId;
        StudentName = studentName;
        Subtotal = tariffs.Sum(tariff => tariff.Amount);
        Discount = discount;
        Areals = areals;
        DateTime = dateTime;
        Tariffs = tariffs;
        Total = total;
    }

}