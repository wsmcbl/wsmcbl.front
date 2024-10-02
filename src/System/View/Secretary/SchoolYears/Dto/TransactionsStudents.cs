using Newtonsoft.Json;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

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