namespace wsmcbl.src.View.Academy.AddGrade;

public class GradesOfEnrollmentsDto
{
    public int partialId { get; set; }
    public string subjectId { get; set; } = null!;
    public List<DetailGradeDto>? grades { get; set; }
}