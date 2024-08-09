using Newtonsoft.Json;

namespace wsmcbl.front.Model.Secretary;
public class TeacherEntity
{
    [JsonProperty ("teacherId")]
    public string? TeacherId { get; set; }
    
    [JsonProperty("name")]
    public string? Name { get; set; }
    
    public bool isGuide { get; set; }
    
    [JsonProperty("phone")]
    public string? Phone { get; set; }
    
    [JsonProperty("guideEnrollment")]
    public EnrollmentEntity? GuideEnrollment { get; set; }
    
    [JsonProperty("subjects")]
    public List<SubjectEntity>? Subjects { get; set; }
}