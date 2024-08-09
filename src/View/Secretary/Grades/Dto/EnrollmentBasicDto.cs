using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.Grades.Dto;

public class EnrollmentBasicDto
{
    public string enrollmentId { get; set; }
    public string teacherId { get; set; }
    public string teacherName { get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    public  List<SubjectBasicDto> subjects { get; set; }

    public EnrollmentEntity ToEntity()
    {
        var result =  new EnrollmentEntity()
        {
            EnrollmentId = enrollmentId,
            TeacherGuide = teacherId == null? true : false,
            Label = label,
            Section = section,
            Capacity = capacity,
            Quantity = quantity,
            Subjects = []
        };
        
        return result;
    }
    
    
}