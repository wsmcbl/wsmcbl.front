using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.Grades.Dto;

public class TeacherBasicDto
{
    public string teacherId { get; set; }
    public string fullName { get; set; }
    public bool isGuide { get; set; }
    
    
    public TeacherEntity ToEntity()
    {
        var result =  new TeacherEntity()
        {
            TeacherId = teacherId,
            Name = fullName,
            Phone = "000",
            GuideEnrollment = null,
            Subjects = [],
            isGuide = isGuide
        };
        
        return result;
    }
}