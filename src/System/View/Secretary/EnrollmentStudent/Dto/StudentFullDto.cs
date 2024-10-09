using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

public class StudentFullDto
{
    public string studentId { get; set; }
    public string name { get; set; }
    public string secondName { get; set; }
    public string surname { get; set; }
    public string secondSurname { get; set; }
    public bool sex { get; set; }
    public DateEntity birthday { get; set; }
    public string religion { get; set; } = null!;
    public string diseases { get; set; } = null!;
    public string address { get; set; } = null!;
    public bool isActive { get; set; }
    public StudentFile file { get; set; }
    public StudentTutor tutor { get; set; }
    public List<StudentParent> parents { get; set; }
    public StudentMeasurements measurements { get; set; }
}