using Newtonsoft.Json;

namespace wsmcbl.front.dto.input;

public class SubjectDto
{
    [JsonProperty("name")] 
    public string Name { get; set; } = null!;
    
    [JsonProperty("isMandatory")] 
    public bool IsMandatory { get; set; }
    
    [JsonProperty("semester")] 
    public int Semester { get; set; }
}