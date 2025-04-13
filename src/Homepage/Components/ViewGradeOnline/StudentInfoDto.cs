namespace wsmcbl.src.Components.ViewGradeOnline;

public class StudentInfoDto
{
    public string studentId { get; set; } = null!;
    public string studentName { get; set; } = null!;
    public string teacherName { get; set; } = "N/A";
    public string enrollment { get; set; } = "N/A";
    public bool hasSolvency { get; set; }
}