using Newtonsoft.Json;
using wsmcbl.front.dto.input;

namespace wsmcbl.front.dto.Output;

public class NewSchoolYearDto
{
    [JsonProperty("grades")]
    public List<GradeDto>? Grades { get; set; }
    
    [JsonProperty("tariffs")]
    public List<SchoolYearTariffs>? Tariffs { get; set; }
}