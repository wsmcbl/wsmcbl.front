namespace wsmcbl.src.Model.Secretary;
public class StudentEntity
{   public string studentId { get; set; }
    public string name { get; set; }
    public string secondName { get; set; }
    public string surname { get; set; }
    public string secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnly? birthday { get; set; }
    public string religion { get; set; }
    public string diseases { get; set; }
    public string address { get; set; }
    public bool isActive { get; set; }
    public StudentFile file { get; set; }
    public StudentTutor tutor { get; set; }
    public List<StudentParent> parents { get; set; }
    public StudentMeasurements measurements { get; set; }
    
    public StudentEntity()
    {
        parents =
        [
            new StudentParent(),
            new StudentParent()
        ];
        tutor = new StudentTutor();
        measurements = new StudentMeasurements();
        file = new StudentFile();
    }
    
    public string FullName()
    {
        return $"{name} {secondName} {surname} {secondSurname}";
    }
}