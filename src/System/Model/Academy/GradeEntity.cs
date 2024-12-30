namespace wsmcbl.src.Model.Academy;

public class GradeEntity
{
    public string subjectId { get; set; } = null!;
    public double Grade { get; set; }
    public double ConductGrade { get; set; }
    public string Label { get; set; } = null!;
}