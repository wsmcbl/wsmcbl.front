using Newtonsoft.Json;
using wsmcbl.front.dto.input;

namespace wsmcbl.front.model.Secretary.Input;

public class SchoolYearEntity
{
    [JsonProperty("id")]
    public string? SchoolYearId { get; set; }

    [JsonProperty("label")]
    public string? Label { get; set; }

    [JsonProperty("startDate")]
    public string? StartDate { get; set; }
    
    [JsonProperty("deadLine")]
    public string? DeadLine { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; }

    [JsonProperty("grades")]
    public List<GradeDto>? Grades { get; set; }
    
    [JsonProperty("tariffs")]
    public List<SchoolYearTariffs>? Tariffs { get; set; }
}