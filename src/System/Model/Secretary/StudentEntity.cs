namespace wsmcbl.src.Model.Secretary;

public class StudentEntity
{   public string? studentId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnly? birthday { get; set; }
    public bool isActive { get; set; }
    public string? religion { get; set; }
    public string? diseases { get; set; }
    public string? address { get; set; }
    public StudentFile? file { get; set; }
    public StudentTutor? tutor { get; set; }
    public List<StudentParent>? parents { get; set; }
    public StudentMeasurements? measurements { get; set; }
    public string? minedId { get; set; }
    public string? profilePicture { get; set; }
    
    public StudentEntity()
    {
        parents =
        [
            new StudentParent(),
            new StudentParent()
        ];
        tutor = new StudentTutor();
        file = new StudentFile();
        
        measurements = new StudentMeasurements();
        measurements.SetDefaultValues();
    }
    
    public string FullName()
    {
        return $"{name} {secondName} {surname} {secondSurname}";
    }

    public bool IsStudentValid()
    {
        return !string.IsNullOrWhiteSpace(diseases)
               && !string.IsNullOrWhiteSpace(religion)
               && !string.IsNullOrWhiteSpace(address)
               && !string.IsNullOrWhiteSpace(name)
               && !string.IsNullOrWhiteSpace(surname);
    }

    public bool IsTutorValid() => tutor!.IsTutorValid();
    
    public bool IsMeasurementsValid() => measurements!.IsMeasurementsValid();
}