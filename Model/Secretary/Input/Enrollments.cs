using Newtonsoft.Json;
using wsmcbl.front.model.Academy.Input;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.model.Secretary.Input;

public class Enrollments
{
    [JsonProperty("enrollmentId")]
    public string? EnrollmentId { get; set; } = null!;
    
    [JsonProperty("label")]
    public string Label { get; set; } = null!;

    [JsonProperty("schoolYear")] 
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("section")]
    public string? Section { get; set; }
    
    [JsonProperty("capacity")]
    public int Capacity { get; set; }
    
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
    
    [JsonProperty("gradeId")]
    public int GradeId { get; set; }
    
    [JsonProperty("state")]
    public bool State { get; set; }
    
    
    [JsonProperty("students")]
    public List<StudentEntity> Students { get; set; } = null!;
    
    [JsonProperty("teacherGuide")]
    public Teacher TeacherGuide { get; set; } = null!;
    
    [JsonProperty("subjects")]
    public List<Subject> Subjects { get; set; } = null!;
    
}