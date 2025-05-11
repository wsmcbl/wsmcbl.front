using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.AddGrade;

public class TeacherDto
{
    public string teacherId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isGuide { get; set; }
    public string enrollmentId { get; set; } = null!;
    public string enrollmentLabel {get; set;} = null!;

    
    public TeacherEntity toEntity()
    {
        var enrollment = new EnrollmentEntity
        {
            enrollmentId = enrollmentId,
            label = enrollmentLabel
        };

        return new TeacherEntity
        {
            teacherId = teacherId,
            fullName = fullName,
            isGuide = isGuide,
            enrollment = enrollment
        };
    }
}