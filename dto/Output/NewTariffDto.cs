using Newtonsoft.Json;
using wsmcbl.front.dto.input;

namespace wsmcbl.front.dto.Output;

public class NewTariffDto
{
    [JsonProperty("concept")]
    public string? Concept { get; set; }
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    [JsonProperty("dueDate")]
    public Date? DueDate { get; set; }
    
    [JsonProperty("typeId")]
    public int Type { get; set; }
    
    [JsonProperty("modality")]
    public int Modality { get; set; }
}