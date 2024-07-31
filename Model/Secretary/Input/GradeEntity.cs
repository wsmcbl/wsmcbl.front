using Newtonsoft.Json;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.Model.Secretary.Input;

public class GradeEntity
{
    [JsonProperty("gradeId")]
    public string GradeId { get; set; }
    
    [JsonProperty("label")]
    public string Label { get; set; } = null!;
    
    [JsonProperty("schoolYear")]
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
    
    [JsonProperty("modality")]
    public string Modality { get; set; } = null!;

    [JsonProperty("sections")]
    public List<EnrollmentEntity> Sections { get; set; } = null!;
    
    [JsonProperty("subjects")]
    public List<SubjectDto>? Subjects { get; set; }

}