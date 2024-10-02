using Newtonsoft.Json;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class PaymentDto
{
    [JsonProperty("tariffId")]
    public int TariffId { get; set; }
    
    [JsonProperty("concept")]
    public string Concept { get; set; } = null!;

    [JsonProperty("amount")]
    public double Amount { get; set; }

    [JsonProperty("discount")]
    public double Discount { get; set; }

    [JsonProperty("arrears")]
    public double Arrears { get; set; }
    
    [JsonProperty("debtBalance")]
    public double DebtBalance { get; set; }

}

