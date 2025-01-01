using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.AddGrade;

public class SaveGradeDto
{
    public TeacherEnrollmentByPartialDto teacherEnrollment { get; set; } = null!;
    public List<GradeEntity> gradeList { get; set; } = null!;
}


