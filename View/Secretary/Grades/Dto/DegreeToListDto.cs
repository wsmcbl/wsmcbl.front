using Newtonsoft.Json;
using wsmcbl.front.Model.Secretary;

namespace wsmcbl.front.View.Secretary.Grades.Dto;

public class DegreeToListDto
{
    [JsonProperty("degreeId")]  
    public string DegreeId { get; set; }
    
    [JsonProperty("label")]  
    public string Label { get; set; }
    
    [JsonProperty("modality")]  
    public string Modality { get; set; }

    public DegreeEntity ToEntity()
    {
        var result =  new DegreeEntity
        {
            GradeId = DegreeId,
            Label = Label,
            Modality = Modality,
            Enrollments = [],
            SchoolYear = ""
        };
        
        return result;
    }
    
}