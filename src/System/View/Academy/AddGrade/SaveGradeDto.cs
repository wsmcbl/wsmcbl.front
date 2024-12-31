namespace wsmcbl.src.View.Academy.AddGrade;

public class SaveGradeDto
{
    public TeacherEnrollment teacherEnrollment { get; set; } = null!;
    public List<Grade> gradeList { get; set; } = null!;
}

public class TeacherEnrollment
{
    public int partialId { get; set; }
    public string enrollmentId { get; set; } = null!;
    public string teacherId { get; set; } = null!;
}

public class Grade
{
    public int gradeId { get; set; }
    public string studentId { get; set; } = null!;
    public int grade { get; set; }
    public int conductGrade { get; set; }
    public string? label { get; set; }
}


