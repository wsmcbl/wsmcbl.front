namespace wsmcbl.src.View.Secretary.AddGradeToStudentReturning;

public class GradeDto
{
    public string studentId { get; set; } = string.Empty;
    public List<GradeDetailDto> grades { get; set; } = [];
}

public class GradeDetailDto
{
    public int subjectPartialId { get; set; }
    public decimal grade { get; set; }
    public decimal conductgrade { get; set; }
}