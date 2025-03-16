using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.Schoolyear.Dto;

public class BasicSchoolyearDto
{ 
    [JsonProperty("schoolyearId")] 
    public string SchoolyearId { get; set; } = null!;
    
    [JsonProperty("label")] 
    public string Label { get; set; } = null!;
    
    [JsonProperty("startDate")] 
    public string StartDate { get; set; } = null!;
    
    [JsonProperty("deadLine")] 
    public string DeadLine { get; set; } = null!;

    [JsonProperty("isActive")] 
    public bool IsActive { get; set; }
}