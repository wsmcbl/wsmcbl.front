namespace wsmcbl.src.View.Academy.AddGrade;

public class StudentSubjectGradeDto
{
    public string StudentId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Dictionary<string, GradeInfo> SubjectGrades { get; set; } = new();
    
    
    public class GradeInfo
    {
        public double Grade { get; set; }
        public double ConductGrade { get; set; }
        public string Label { get; set; } = null!;
    }
}