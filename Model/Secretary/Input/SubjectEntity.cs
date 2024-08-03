using Newtonsoft.Json;

namespace wsmcbl.front.Model.Secretary.Input;
public class SubjectEntity
{
    [JsonProperty("subjectId")]
    public string? SubjectId { get; set; }
    
    [JsonProperty("name")]
    public string? Name { get; set; } 
    
    [JsonProperty("isMandatory")]
    public bool IsMandatory { get; set; }
    
    [JsonProperty("semester")]
    public int Semester { get; set; }
    
    [JsonProperty("teacherId")] public string TeacherId { get; set; }
    
}