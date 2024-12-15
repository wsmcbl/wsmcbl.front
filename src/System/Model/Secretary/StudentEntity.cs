using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

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
        measurements = new StudentMeasurements();
        file = new StudentFile();
    }

    public StudentEntity(StudentFullDto dto)
    {
        studentId = dto.studentId;
        minedId = dto.minedId;
        name = dto.name;
        secondName = dto.secondName;
        surname = dto.surname;
        secondSurname = dto.secondSurname;
        sex = dto.sex;
        birthday = new DateOnly(dto.birthday.year, dto.birthday.month, dto.birthday.day);
        isActive = dto.isActive;
        religion = dto.religion;
        diseases = dto.diseases;
        address = dto.address;
        profilePicture = dto.profilePicture;
        file = dto.file;
        tutor = dto.tutor;
        parents = dto.parentList;
        measurements = dto.measurements;
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