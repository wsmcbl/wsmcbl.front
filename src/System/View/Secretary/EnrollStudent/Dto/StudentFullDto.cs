using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.EnrollStudent.Dto;

public class StudentFullDto
{
    public string studentId { get; set; }
    public string? minedId { get; set; }
    public string name { get; set; }
    public string? secondName { get; set; }
    public string surname { get; set; }
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateEntity birthday { get; set; }
    public bool isActive { get; set; }
    public string religion { get; set; } = null!;
    public string diseases { get; set; } = null!;
    public string address { get; set; } = null!;
    public string? profilePicture { get; set; }
    
    public StudentFile file { get; set; }
    public StudentTutor tutor { get; set; }
    public List<StudentParent>? parentList { get; set; }
    public StudentMeasurements measurements { get; set; }
    
    public StudentFullDto()
    {
    }
    
    public StudentFullDto(StudentEntity student)
    {
        studentId = student.studentId;
        name = student.name;
        secondName = string.IsNullOrWhiteSpace(student.secondName) ? null : student.secondName;
        surname = student.surname;
        secondSurname = string.IsNullOrWhiteSpace(student.secondSurname) ? null : student.secondSurname;
        sex = student.sex;
        birthday = student.birthday.ToEntityOrNull()!;
        isActive = student.isActive;
        religion = student.religion;
        diseases = student.diseases;
        address = student.address;
        file = student.file;
        tutor = student.tutor;
        
        student.parents!.RemoveAll(t => t.isTutorEmpty());
        if (student.parents.Any())
        {
            parentList = student.parents;
        }
        
        measurements = student.measurements;
    }

    public StudentEntity ToEntity()
    {
        return new StudentEntity
        {
            studentId = studentId,
            name = name,
            secondName = secondName,
            surname = surname,
            secondSurname = secondSurname,
            sex = sex,
            birthday = birthday.ToDateOnly(),
            isActive = isActive,
            religion = religion,
            diseases = diseases,
            address = address,
            file = file,
            tutor = tutor,
            parents = parentList.ToListEntity(),
            measurements = measurements,
            minedId = minedId,
            profilePicture = profilePicture
        };
    }
}