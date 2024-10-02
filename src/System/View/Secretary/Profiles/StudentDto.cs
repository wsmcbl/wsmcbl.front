using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.Profiles;
public class StudentDto
{
    [JsonProperty("studentId")]
    public string StudentId { get; set; } = null!;
    
    [JsonProperty("name")]
    public string Name { get; set; } = null!;
    
    [JsonProperty("secondName")]
    public string? SecondName { get; set; }
    
    [JsonProperty("surname")]
    public string Surname { get; set; } = null!;
    
    [JsonProperty("secondSurname")]
    public string? SecondSurname { get; set; }
    
    [JsonProperty("sex")]
    public bool Sex { get; set; } 
    
    [JsonProperty("birthday")]
    public DateOnly Birthday { get; set; }
    
    [JsonProperty("tutor")]
    public string Tutor { get; set; } = null!;
    
    [JsonProperty("modality")]
    public int Modality { get; set; }
        
}
