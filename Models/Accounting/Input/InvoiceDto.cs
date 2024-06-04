namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;

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

    [JsonProperty("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonProperty("subtotal")]
    public double Subtotal { get; set; }

    [JsonProperty("generalBalance")]
    public float[] GeneralBalance { get; set; }

    [JsonProperty("tariffs")]
    public List<TariffStudents> Tariffs { get; set; } = null!;

    public InvoiceDto(string transactionId, string cashierName, string studentId,  string studentName, double total, DateTime dateTime, List<TariffStudents> tariffs)
    {
        TransactionId = transactionId;
        CashierName = cashierName;
        StudentId = studentId;
        StudentName = studentName;
        Subtotal = tariffs.Sum(tariff => tariff.Amount);
        DateTime = dateTime;
        Tariffs = tariffs;
        Total = total;
    }
}