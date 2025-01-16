namespace wsmcbl.src.Model.Academy;

public class SubjectEntity
{
    public string? subjectId { get; set; }
    public string? Initials { get; set; }
    public string? name { get; set; } 
    public bool IsMandatory { get; set; }
    public int Semester { get; set; }
}