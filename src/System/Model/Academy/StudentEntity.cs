namespace wsmcbl.src.Model.Academy;

public class StudentEntity
{
    public string studentId { get; set; }
    public string name { get; set; }
    public bool sex { get; set; }
    
    protected Secretary.StudentEntity student { get; set; }

    public string FullName() => student.FullName();
}