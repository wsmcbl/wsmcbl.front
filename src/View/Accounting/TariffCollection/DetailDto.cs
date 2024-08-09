using Newtonsoft.Json;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class DetailDto
{
    [JsonProperty("tariffId")]
    public int tariffId { get; set; }
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    [JsonProperty("arrears")]
    public bool applyArrear { get; set; }
}