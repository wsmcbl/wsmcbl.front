using Newtonsoft.Json;

namespace wsmcbl.front.dto.input;

public class TransactionsStudents
{
    [JsonProperty("transactionId")]
    public string TransactionId { get; set; } = null!;

    [JsonProperty("studentId")]
    public string StudentId { get; set; } = null!;

    [JsonProperty("cashierId")]
    public string CashierId { get; set; } = null!;

    [JsonProperty("total")]
    public double Total { get; set; }

    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("details")]
    public List<TransactionDto> Details { get; set; } = null!;
}