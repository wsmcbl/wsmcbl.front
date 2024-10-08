using Newtonsoft.Json;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class InvoiceDto
{
    [JsonProperty("transactionId")]
    public string? TransactionId { get; set; } 

    [JsonProperty("cashierName")]
    public string? CashierName { get; set; }

    [JsonProperty("studentId")]
    public string? StudentId { get; set; }
    
    [JsonProperty("studentName")]
    public string? StudentName { get; set; }

    [JsonProperty("total")]
    public double Total { get; set; }

    [JsonProperty("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonProperty("subtotal")]
    public double Subtotal { get; set; }

    [JsonProperty("generalBalance")]
    public float[]? GeneralBalance { get; set; }

    [JsonProperty("detail")]
    public List<TransactionDto>? detail { get; set; }

    public float getGeneralBalance()
    {
        return GeneralBalance[1] - GeneralBalance[0];
    }
}