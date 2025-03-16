namespace wsmcbl.src.View.Secretary.Schoolyear;

public class DegreeSubjectDto
{
    public string label { get; set; } = null!;
    public string educationalLevel { get; set; } = null!;
    public string tag { get; set; } = null!;
    public List<SubjectDto> subjectList { get; set; } = null!;
}