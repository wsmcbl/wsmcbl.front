using Newtonsoft.Json;

namespace wsmcbl.front.Models.Accounting.Output;

public class DetailDto
{
    [JsonProperty("tariffId")]
    public int tariffId { get; set; }
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    [JsonProperty("arrears")]
    public double arrears { get; set; }
}