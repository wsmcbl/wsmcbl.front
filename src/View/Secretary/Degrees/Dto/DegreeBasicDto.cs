using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class DegreeBasicDto
{
    public string degreeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public string modality { get; set; }
    public List<EnrollmentBasicDto> enrollments {get; set;}
    public List<SubjectDto> subjects { get; set; }
    
    public DegreeEntity toEntity()
    {
        var result = new DegreeEntity()
        {
            GradeId = degreeId,
            Label = label,
            SchoolYear = schoolYear,
            Quantity = quantity,
            Sections = 0,
            Modality = modality,
            Enrollments = enrollments,
            Subjects = subjects?.Select(s => s.ToEntity()).ToList()
        };
        return result;
    }
    
    
    
}