using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class DegreeToListDto
{
    public string degreeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public string modality { get; set; }
    
    public DegreeEntity ToEntity()
    {
        var result =  new DegreeEntity
        {
            DegreeId = degreeId,
            Label = label,
            Modality = modality,
            Enrollments = [],
            SchoolYear = schoolYear,
            Quantity = quantity
        };
        
        return result;
    }
    
}