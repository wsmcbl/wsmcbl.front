using Newtonsoft.Json;

namespace wsmcbl.front.Model.Secretary;

public class EnrollmentEntity
{
    [JsonProperty("enrollmentId")]
    public string EnrollmentId { get; set; }
    
    [JsonProperty("label")]
    public string Label { get; set; } = null!;
    
    [JsonProperty("schoolYear")] 
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("capacity")]
    public int Capacity { get; set; }
    
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
    
    [JsonProperty("section")]
    public string Section { get; set; }
    
    [JsonProperty("students")]
    public List<StudentEntity> Students { get; set; } = null!;
    
    [JsonProperty("teacherGuide")]
    public bool TeacherGuide { get; set; }
    
    [JsonProperty("subjects")]
    public List<SubjectEntity> Subjects { get; set; } = null!;
    
}