using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class UpdateEnrollmentDto
{
    public List<EnrollmentEntity> enrollmentList { get; set; } = null!;
    public List<TeacherEntity> teacherList { get; set; } = null!;
    public List<EnrollmentEntity.MinimalSubject> subjectList { get; set; } = null!;
}