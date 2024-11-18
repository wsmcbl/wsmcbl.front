namespace wsmcbl.src.Model.Academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public string schoolyear { get; set; } = null!;
    public string enrollment { get; set; } = null!;
}