using Newtonsoft.Json;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.model.Academy.Input;

public class EnrollmentEntity
{
    [JsonProperty("enrollmentId")]
    public string? EnrollmentId { get; set; } = null!;
    
    [JsonProperty("label")]
    public string Label { get; set; } = null!;

    [JsonProperty("schoolYear")] 
    public string SchoolYear { get; set; } = null!;

    [JsonProperty("students")]
    public List<StudentEntity> Students { get; set; } = null!;
    
    [JsonProperty("teacherGuide")]
    public Teacher TeacherGuide { get; set; } = null!;
    
    [JsonProperty("subjects")]
    public List<Subject> Subjects { get; set; } = null!;
    
    [JsonProperty("capacity")]
    public int Capacity { get; set; }
    
}