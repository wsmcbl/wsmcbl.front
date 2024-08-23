namespace wsmcbl.src.Model.Academy;

public class TeacherEntity
{
    public string? TeacherId { get; set; }
    public string? Name { get; set; }
    public bool isGuide { get; set; }
    public string? Phone { get; set; }
    public EnrollmentEntity? Enrollment { get; set; }
    public List<SubjectEntity>? SubjectList { get; set; }
}