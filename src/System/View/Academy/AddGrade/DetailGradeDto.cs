namespace wsmcbl.src.View.Academy.AddGrade;

public class DetailGradeDto
{
    public int gradeId { get; set; }
    public string studentId { get; set; } = null!;
    public double grade { get; set; }
    public double conductGrade { get; set; }
    public string label { get; set; } = null!;
}