namespace wsmcbl.src.View.Academy.PerformanceReportBySection;

public class StudentsGradeSumaryDto
{
    public string studentId { get; set; } = null!;
    public string minedId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool sex { get; set; } 
    public List<GradeDto> gradeList { get; set; } = null!;
    public decimal gradeAverage { get; set; }
    public string labelAverage { get; set; } = null!;
    public decimal conductGrade { get; set; }
    public string conductGradeLabel { get; set; } = null!;

    public class GradeDto
    {
        public string subjectId { get; set; } = null!;
        public decimal grade { get; set; }
        public decimal conductGrade { get; set; }
        public string label { get; set; } = null!;
    }
    
}