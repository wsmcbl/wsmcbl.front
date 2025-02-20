namespace wsmcbl.src.View.Management.PartialsGradeRecording;

public class PartialListDto
{
    public int partialId { get; set; }
    public string label { get; set; } = null!;
    public bool isActive { get; set; }
    public string semester { get; set; } = null!; 
    public string period { get; set; } = null!; 
}