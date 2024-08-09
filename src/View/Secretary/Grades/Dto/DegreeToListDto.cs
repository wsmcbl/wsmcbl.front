using wsmcbl.front.Model.Secretary;
namespace wsmcbl.front.View.Secretary.Grades.Dto;

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
            GradeId = degreeId,
            Label = label,
            Modality = modality,
            Enrollments = [],
            SchoolYear = schoolYear,
            Quantity = quantity
        };
        
        return result;
    }
    
}