namespace wsmcbl.src.View.Secretary.EnrollStudent.Dto;

public class DegreeBasicDto
{
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string educationalLevel { get; set; }
    public int position { get; set; }
    public List<EnrollmentsBasicDto>? enrollments { get; set; }
}