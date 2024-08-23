namespace wsmcbl.src.Model.Secretary;
public class StudentEntity
{   
    public string StudentId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SecondName { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string SecondSurname { get; set; } = null!;
    public bool IsActive { get; set; }
    public string SchoolYear { get; set; } = null!;
    public string Tutor { get; set; } = null!;
    public bool Sex { get; set; }
    public DateOnly Birthday { get; set; }
    public string EnrollmentLabel { get; set; } = null!;

    public string FullName()
    {
        return $"{Name} {SecondName} {Surname} {SecondSurname}";
    }
}