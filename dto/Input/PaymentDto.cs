namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;

public class PaymentDto
{
    [JsonProperty("tariffId")]
    public int TariffId { get; set; }

    [JsonProperty("schoolYear")]
    public string SchoolYear { get; set; } = null!;

    [JsonProperty("concept")]
    public string Concept { get; set; } = null!;

    [JsonProperty("amount")]
    public double Amount { get; set; }

    [JsonProperty("discount")]
    public double Discount { get; set; }

    [JsonProperty("arrears")]
    public double Arrears { get; set; }

    [JsonProperty("subTotal")]
    public double SubTotal { get; set; }

    [JsonProperty("debtBalance")]
    public double DebtBalance { get; set; }

    [JsonProperty("isPaidLate")]
    public bool IsPaidLate { get; set; }

    public PaymentDto(int tariffId, string concept, double amount, double discount, double arrears)
    {
        TariffId = tariffId;
        Concept = concept;
        Amount = amount;
        Discount = discount;
        Arrears = arrears;
    }
    
    
    
}

