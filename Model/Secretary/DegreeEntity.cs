using Newtonsoft.Json;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.Model.Secretary;

public class DegreeEntity
{ //revisado y cotejado con Back
    [JsonProperty("degreeId")]  
    public string GradeId { get; set; }
    
    [JsonProperty("label")]
    public string Label { get; set; } = null!;
    
    [JsonProperty("schoolYear")]
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    public int Sections { get; set; }
    
    [JsonProperty("modality")]
    public string Modality { get; set; } = null!;

    [JsonProperty("enrollments")]
    public List<EnrollmentEntity> Enrollments { get; set; } = null!;
    
    [JsonProperty("subjects")]
    public List<SubjectDto>? Subjects { get; set; }
}