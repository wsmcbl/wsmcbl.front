namespace wsmcbl.src.View.Secretary.EnrollStudent.Dto;

public class DegreeBasicDto
{
    public string degreeId { get; set; }
    public string label { get; set; }
    public string educationalLevel { get; set; }
    public int position { get; set; }
    public List<EnrollmentsBasicDto> enrollments { get; set; }
}