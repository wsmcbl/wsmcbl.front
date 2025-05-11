namespace wsmcbl.src.Model.Academy;

public class GradeEntity
{
    public int gradeId { get; set; }
    public string? studentId { get; set; }
    public string? subjectId { get; set; }
    public decimal grade { get; set; }
    public decimal conductGrade { get; set; }
    public string label { get; set; } = null!;

    public bool HasValidGrade()
    {
        return IsValidGradeValue(grade);
    }

    public bool HasValidConductGrade()
    {
        return IsValidGradeValue(conductGrade);
    }
    
    public static bool IsValidGradeValue(decimal value)
    {
        return value is >= 0 and <= 100;
    }
}