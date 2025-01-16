using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class EnrollmentBasicDto
{
    public string enrollmentId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
    public string teacherName { get; set; } = null!;
    public string label { get; set; } = null!;
    public string section { get; set; } = null!;
    public int capacity { get; set; }
    public int quantity { get; set; }
    public List<StudentEntity> studentList { get; set; } = [];
    public List<SubjectBasicDto> subjects { get; set; } = [];

    public EnrollmentEntity ToEntity(List<SubjectEntity> subjectList, List<TeacherEntity> teacherList)
    {
        var result = new EnrollmentEntity()
        {
            enrollmentId = enrollmentId,
            teacherId = teacherId,
            label = label,
            section = section,
            capacity = capacity,
            quantity = quantity
        };

        result.SetSubjectTeacherList(getSubjectTeacherTuple(subjectList, teacherList));

        return result;
    }

    private List<(SubjectEntity subject, TeacherEntity teacher)> getSubjectTeacherTuple(List<SubjectEntity> subjectList, List<TeacherEntity> teacherList)
    {
        var list = new List<(SubjectEntity subject, TeacherEntity teacher)>();

        foreach (var subjectDto in subjects)
        {
            var subjectEntity = subjectList.FirstOrDefault(e => e.subjectId == subjectDto.subjectId);
            var teacherEntity = teacherList.FirstOrDefault(e => e.teacherId == subjectDto.teacherId)
                                ?? new NullTeacherEntity();

            list.Add((subjectEntity!, teacherEntity));
        }
        
        return list;
    }
}