using Newtonsoft.Json;
namespace wsmcbl.front.View.Secretary.SchoolYears.Dto;
    
public class NewSchoolYearDto
{
    [JsonProperty("degrees")]
    public List<GradeDto>? Grades { get; set; }
    
    [JsonProperty("tariffs")]
    public List<SchoolYearTariffs>? Tariffs { get; set; }
}