using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class SchoolYearTariffs
{
    [JsonProperty("schoolYear")]
    public string? SchoolYear { get; set; }
    
    [JsonProperty("concept")]
    public string? Concept { get; set; }
    
    [JsonProperty("amount")]
    public double Amount { get; set; }
    
    [JsonProperty("dueDate")]
    public DateClass? DueDate { get; set; }
    
    public DateOnly OnlyDate { get; set; }
    
    [JsonProperty("type")]
    public int Type { get; set; }
    
    [JsonProperty("modality")]
    public int Modality { get; set; }
}