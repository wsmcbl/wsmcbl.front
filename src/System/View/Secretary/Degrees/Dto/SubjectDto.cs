using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class SubjectDto
{
    public string? subjectId { get; set; }
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;
    
    public SubjectEntity ToEntity()
    {
        return new SubjectEntity()
        {
            subjectId = subjectId,
            Initials = initials,
            name = name,
            IsMandatory = isMandatory,
            Semester = semester
        };
    }
}