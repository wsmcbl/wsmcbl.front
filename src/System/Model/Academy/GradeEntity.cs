namespace wsmcbl.src.Model.Academy;

public class GradeEntity
{
    public string? studentId { get; set; }
    public string? subjectId { get; set; }
    public int Grade { get; set; }
    public int ConductGrade { get; set; }
    public string Label { get; set; } = null!;
}