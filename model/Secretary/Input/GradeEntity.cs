using Newtonsoft.Json;
using wsmcbl.front.model.Academy.Input;

namespace wsmcbl.front.model.Secretary.Input;

public class GradeEntity
{
    [JsonProperty("gradeId")]
    public int GradeId { get; set; }
    
    [JsonProperty("label")]
    public string Label { get; set; } = null!;
    
    [JsonProperty("schoolYear")]
    public string SchoolYear { get; set; } = null!;
    
    [JsonProperty("quantity")]
    public int Quantity { get; set; }
    
    [JsonProperty("modality")]
    public string Modality { get; set; } = null!;

    [JsonProperty("sections")]
    public List<Enrollments> Sections { get; set; } = null!;

}