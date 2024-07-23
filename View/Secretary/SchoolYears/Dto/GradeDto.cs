using Newtonsoft.Json;

namespace wsmcbl.front.View.Secretary.SchoolYears.Dto;

public class GradeDto
{
    [JsonProperty("label")] 
    public string Label { get; set; } = null!;
    
    [JsonProperty("schoolYear")] 
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("modality")] 
    public string Modality { get; set; } = null!;
    
    [JsonProperty("subjects")] 
    public List<SubjectDto> Subjects { get; set; } = null!;
}