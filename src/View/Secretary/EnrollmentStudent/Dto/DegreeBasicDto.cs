namespace wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

public class DegreeBasicDto
{
    public string degreeId { get; set; }
    public string label { get; set; }
    public string modality { get; set; }
    public List<EnrollmentsBasicDto> enrollments { get; set; }
}