using Newtonsoft.Json;

namespace wsmcbl.front.dto.input;

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
    public float[]? GeneralBalance { get; set; }

    [JsonProperty("detail")]
    public List<TransactionDto> detail { get; set; } = null!;

    public float getGeneralBalance()
    {
        return GeneralBalance[1] - GeneralBalance[0];
    }
}