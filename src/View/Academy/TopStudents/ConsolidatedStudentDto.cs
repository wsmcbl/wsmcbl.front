namespace wsmcbl.src.View.Academy.TopStudents;

public class ConsolidatedStudentDto
{
    public string StudentId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Dictionary<int, PartialGrade> PartialGrades { get; set; } = new();
    public string? FinalGrade { get; set; }
}

public class PartialGrade
{
    public decimal Grade { get; set; }
    public string Label { get; set; } = null!;
    public bool HasGrade => Grade > 0; // Para saber si tiene calificación
}