using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class SubjectTeacherDto
{
    public string subjectId { get; set; }
    public string teacherId { get; set; }

    public SubjectTeacherDto(string teacherId, string subjectId)
    {
        this.subjectId = subjectId;
        this.teacherId = teacherId;
    }

    public SubjectTeacherDto(SubjectEntity subject, TeacherEntity teacher) : this(teacher.teacherId!, subject.SubjectId!)
    {
    }
}