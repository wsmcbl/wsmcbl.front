namespace wsmcbl.src.Model.Academy;

public class GradeEntity
{
    public int gradeId { get; set; }
    public string? studentId { get; set; }
    public string? subjectId { get; set; }
    public decimal grade { get; set; }
    public decimal conductGrade { get; set; }
    public string label { get; set; } = null!;
}