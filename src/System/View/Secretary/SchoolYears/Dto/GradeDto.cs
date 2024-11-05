namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class GradeDto
{
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public string modality { get; set; } = null!;
    public string tag { get; set; } = null!;
    public List<SubjectDto> subjects { get; set; } = null!;
}