using Newtonsoft.Json;

namespace wsmcbl.front.View.Secretary.EnrollmentStudent.Dto;

public class StudentDto
{
    [JsonProperty("studentId")] 
    public string? StudentId { get; set; }
    
    [JsonProperty("fullname")]
    public string? Fullname { get; set; }
    
    [JsonProperty("schoolyear")]
    public string? Schoolyear { get; set; }
}