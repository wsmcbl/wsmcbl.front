using Newtonsoft.Json;

namespace wsmcbl.front.Model.Secretary.Input;
public class SubjectEntity
{
    [JsonProperty("subjectId")]
    public string? SubjectId { get; set; }
    
    [JsonProperty("name")]
    public string? Name { get; set; } 
    
    [JsonProperty("description")]
    public string? Description { get; set; }
    
    [JsonProperty("modality")]
    public string? Modality { get; set; }
    
    [JsonProperty("teacherId")] public string TeacherId { get; set; }
    
}