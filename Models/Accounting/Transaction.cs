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