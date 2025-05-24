namespace wsmcbl.src.View.Academy.PorcentageSubjectEvaluated;

public class SubjectStats
{
    public string subjectId { get; set; } = "N/A";
    public string enrollmentId { get; set; } = "N/A";
    public StudentCount studentsCount { get; set; } = new();
    public StudentCount studentsNotEvaluated { get; set; } = new();
}

public class StudentCount
{
    public int total { get; set; }
    public int males { get; set; }
    public int females { get; set; }
}