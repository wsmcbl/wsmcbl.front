using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class UpdateEnrollmentDto
{
    public List<EnrollmentEntity>? EnrollmentList { get; set; }
    public List<TeacherEntity>? TeacherList { get; set; }
    public List<EnrollmentEntity.MinimalSubject>? subjectList { get; set; }
}