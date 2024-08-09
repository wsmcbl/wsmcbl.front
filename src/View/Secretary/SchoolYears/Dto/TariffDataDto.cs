using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.dto.Output;

public class TariffDataDto
{
    [JsonProperty("concept")]
    public string? Concept { get; set; }
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    public Date? dueDate { get; set; }
    
    [JsonProperty("typeId")]
    public int Type { get; set; }
    
    [JsonProperty("modality")]
    public int Modality { get; set; }
    
}