using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class EnrollmentBasicDto
{
    public string enrollmentId { get; set; }
    public string teacherId { get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    public  List<SubjectBasicDto> subjects { get; set; }

    public EnrollmentEntity ToEntity(List<SubjectEntity> subjectList)
    {
        var result = new EnrollmentEntity()
        {
            enrollmentId = enrollmentId,
            teacherId = teacherId,
            Label = label,
            Section = section,
            Capacity = capacity,
            Quantity = quantity
        };
        
        result.SetSubjectTeacherList(subjectList);

        return result;
    }
}