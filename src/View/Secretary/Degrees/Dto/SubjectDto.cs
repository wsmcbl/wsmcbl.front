using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class SubjectDto
{
    public string subjectId { get; set; }
    public string name { get; set; }
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; }
    
    public SubjectEntity ToEntity()
    {
        return new SubjectEntity()
        {
            SubjectId = subjectId,
            Initials = initials,
            Name = name,
            IsMandatory = isMandatory,
            Semester = semester,
            TeacherId = null
        };
    }
    
    public SubjectBasicDto ToBasicDto()
    {
        return new SubjectBasicDto
        {
            subjectId = subjectId,
            teacherId = null 
        };
    }
}