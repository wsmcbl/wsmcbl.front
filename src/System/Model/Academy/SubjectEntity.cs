namespace wsmcbl.src.Model.Academy;

public class SubjectEntity
{
    public string? SubjectId { get; set; }
    public string Initials { get; set; } = null!;
    public string? Name { get; set; } 
    public bool IsMandatory { get; set; }
    public int Semester { get; set; }
}