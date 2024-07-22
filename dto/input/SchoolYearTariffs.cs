using Newtonsoft.Json;

namespace wsmcbl.front.dto.input;

public class SchoolYearTariffs
{
    [JsonProperty("schoolYear")]
    public string? SchoolYear { get; set; }
    
    [JsonProperty("concept")]
    public string? Concept { get; set; }
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    [JsonProperty("dueDate")]
    public Date? DueDate { get; set; }
    
    [JsonProperty("type")]
    public int Type { get; set; }
    
    [JsonProperty("modality")]
    public int Modality { get; set; }
}