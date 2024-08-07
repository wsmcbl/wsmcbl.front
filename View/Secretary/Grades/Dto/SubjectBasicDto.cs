using wsmcbl.front.Model.Secretary;

namespace wsmcbl.front.View.Secretary.Grades.Dto;

public class SubjectBasicDto
{
    public string subjectId { get; set; }
    public string teacherId { get; set; }
    
    public SubjectDto toEntity()
    {
        var result = new SubjectDto()
        {
            subjectId = subjectId
        };
        return result;
    }
}