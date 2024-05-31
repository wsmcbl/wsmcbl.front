namespace wsmcbl.front.Models.Accounting;
using Newtonsoft.Json;

public class TariffPost
{
    [JsonProperty("tariffId")]
    public int tariffId { get; set; }

    [JsonProperty("discount")]
    public double discount { get; set; }

    [JsonProperty("arrears")]
    public double arrears { get; set; }
    
}