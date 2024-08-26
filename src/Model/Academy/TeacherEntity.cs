namespace wsmcbl.src.Model.Academy;

public class TeacherEntity
{
    public string? teacherId { get; set; }
    public string? fullName { get; set; }
    public bool isGuide { get; set; }
    public string? Phone { get; set; }
    
    public List<SubjectEntity>? SubjectList { get; set; }
}