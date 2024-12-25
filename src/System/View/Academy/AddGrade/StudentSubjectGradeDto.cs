namespace wsmcbl.src.View.Academy.AddGrade;

public class StudentSubjectGradeDto
{
    public string StudentId { get; set; } = null!;
    public string StudentName { get; set; } = null!;
    public Dictionary<string, List<int>> SubjectGrades { get; set; } = new();
}