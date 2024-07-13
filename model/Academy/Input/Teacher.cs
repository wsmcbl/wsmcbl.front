using Newtonsoft.Json;

namespace wsmcbl.front.model.Academy.Input;

public class Teacher
{
    [JsonProperty ("teacherId")]
    public string? TeacherId { get; set; }
    
    [JsonProperty("name")]
    public string? Name { get; set; }
    
    [JsonProperty("phone")]
    public string? Phone { get; set; }
    
    [JsonProperty("guideEnrollment")]
    public EnrollmentEntity? GuideEnrollment { get; set; }
    
    [JsonProperty("subjects")]
    public List<Subject>? Subjects { get; set; }
}