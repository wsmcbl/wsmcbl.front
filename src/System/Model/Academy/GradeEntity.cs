namespace wsmcbl.src.Model.Academy;

public class GradeEntity
{
    public string? studentId { get; set; }
    public string? subjectId { get; set; }
    public double Grade { get; set; }
    public double ConductGrade { get; set; }
    public string Label { get; set; } = null!;
}