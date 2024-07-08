using Newtonsoft.Json;

namespace wsmcbl.front.model.Secretary.Output;

public class StudentEntityDto
{
    [JsonProperty("name")]
    public string Name { get; set; } = null!;
    
    [JsonProperty("secondName")]
    public string SecondName { get; set; } = null!;
    
    [JsonProperty("surname")]
    public string Surname { get; set; } = null!;
    
    [JsonProperty("secondSurname")]
    public string SecondSurname { get; set; } = null!;
    
    [JsonProperty("sex")]
    public bool Sex { get; set; }
    
    [JsonProperty("birthday")]
    public Birthday Birthday { get; set; }
    
    [JsonProperty("tutor")]
    public string Tutor { get; set; } = null!;
        
}

public class Birthday
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
}