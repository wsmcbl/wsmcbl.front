using Newtonsoft.Json;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;

namespace wsmcbl.src.Model.Secretary;

public class DegreeEntity
{ //revisado y cotejado con Back
    [JsonProperty("degreeId")]  
    public string DegreeId { get; set; }
    
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
    public List<EnrollmentBasicDto> Enrollments { get; set; } = null!;
    
    [JsonProperty("subjects")]
    public List<SubjectEntity> Subjects { get; set; }
    
}