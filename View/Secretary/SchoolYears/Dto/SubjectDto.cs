using Newtonsoft.Json;

namespace wsmcbl.front.View.Secretary.SchoolYears.Dto;

public class SubjectDto
{
    [JsonProperty("name")] 
    public string Name { get; set; } = null!;
    
    [JsonProperty("isMandatory")] 
    public bool IsMandatory { get; set; }
    
    [JsonProperty("semester")] 
    public int Semester { get; set; }
}