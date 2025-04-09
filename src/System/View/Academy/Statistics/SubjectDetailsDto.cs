namespace wsmcbl.src.View.Academy.Statistics;

public class SubjectDetailsDto
{
    public string subjectId { get; set; } = string.Empty;
    public string teacherId { get; set; } = string.Empty;
    public GenderStats approved { get; set; } = new();
    public GenderStats failed { get; set; } = new();
    public GenderStats notEvaluated { get; set; } = new();
}