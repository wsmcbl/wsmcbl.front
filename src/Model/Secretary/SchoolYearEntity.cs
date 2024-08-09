using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Model.Secretary;
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

    [JsonProperty("degrees")]
    public List<GradeDto>? Degrees { get; set; }
    
    [JsonProperty("tariffs")]
    public List<SchoolYearTariffs>? Tariffs { get; set; }
}