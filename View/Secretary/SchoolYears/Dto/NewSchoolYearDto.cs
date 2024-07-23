using Newtonsoft.Json;
namespace wsmcbl.front.View.Secretary.SchoolYears.Dto;
    
public class NewSchoolYearDto
{
    [JsonProperty("grades")]
    public List<GradeDto>? Grades { get; set; }
    
    [JsonProperty("tariffs")]
    public List<SchoolYearTariffs>? Tariffs { get; set; }
}