using Newtonsoft.Json;

namespace wsmcbl.front.model.Secretary.Input;

public class SchoolYearEntity
{
    [JsonProperty("schoolYearId")]
    public string SchoolYearId { get; set; } = null!;

    [JsonProperty("label")]
    public string Label { get; set; } = null!;

    [JsonProperty("startDate")]
    public DateOnly StartDate { get; set; }
    
    [JsonProperty("deadLine")]
    public DateOnly DeadLine { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
}