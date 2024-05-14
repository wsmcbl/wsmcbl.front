namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;


public class Tariff
{
    [JsonProperty("tariffId")]
    public int TariffId { get; set; }

    [JsonProperty("tariffId")]
    public string Concept { get; set; } = null!;

    [JsonProperty("tariffId")]
    public double Amount { get; set; }

    public Tariff () : this(0,"",0.0)
    {

    }

    public Tariff(int tariffId, string concept, double amount)
    {
        TariffId = tariffId;
        Concept= concept;
        Amount = amount;
    }
}