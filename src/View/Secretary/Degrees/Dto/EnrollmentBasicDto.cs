using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class EnrollmentBasicDto
{
    public string enrollmentId { get; set; }
    public string teacherId { get; set; }
    public string teahcerName { get; set; }
    public string label { get; set; }
    public string section { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    
    public  List<SubjectBasicDto> subjects { get; set; }

    public EnrollmentEntity ToEntity(List<SubjectEntity> subjectList, List<TeacherEntity> teacherList)
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

        result.SetSubjectTeacherList(getSubjectTeacherTuple(subjectList, teacherList));

        return result;
    }

    private List<(SubjectEntity subject, TeacherEntity teacher)> getSubjectTeacherTuple(List<SubjectEntity> subjectList, List<TeacherEntity> teacherList)
    {
        var list = new List<(SubjectEntity subject, TeacherEntity teacher)>();

        foreach (var subjectDto in subjects)
        {
            var subjectEntity = subjectList.FirstOrDefault(e => e.SubjectId == subjectDto.subjectId);
            var teacherEntity = teacherList.FirstOrDefault(e => e.teacherId == subjectDto.teacherId);

            if (teacherEntity == null)
            {
                teacherEntity = new NullTeacherEntity();
            }
            
            list.Add((subjectEntity!, teacherEntity));
        }
        
        return list;
    }
}