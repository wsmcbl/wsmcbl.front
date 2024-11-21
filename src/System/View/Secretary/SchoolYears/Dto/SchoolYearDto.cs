using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class SchoolYearDto
{ 
    [JsonProperty("schoolYearId")] 
    public string SchoolYearId { get; set; } = null!;
    
    [JsonProperty("label")] 
    public string Label { get; set; } = null!;
    
    [JsonProperty("startDate")] 
    public string StartDate { get; set; } = null!;
    
    [JsonProperty("deadLine")] 
    public string DeadLine { get; set; } = null!;

    [JsonProperty("isActive")] 
    public bool IsActive { get; set; }
}