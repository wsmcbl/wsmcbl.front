using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.EnrollStudent.Dto;

public class StudentFullDto
{
    public string? studentId { get; set; } = null!;
    public string? minedId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnlyDto birthday { get; set; } = null!;
    public bool isActive { get; set; }
    public string? religion { get; set; } = null!;
    public string? diseases { get; set; } = null!;
    public string? address { get; set; } = null!;
    public string? profilePicture { get; set; }
    
    public StudentFile? file { get; set; }
    public StudentTutor tutor { get; set; } = null!;
    public List<StudentParent>? parentList { get; set; }
    public StudentMeasurements? measurements { get; set; }
    
    public StudentFullDto()
    {
    }
    
    public StudentFullDto(StudentEntity student)
    {
        studentId = student.studentId!;
        minedId = student.minedId;
        
        name = student.name;
        secondName = student.secondName.GetValueOrNull();
        surname = student.surname;
        secondSurname = student.secondSurname.GetValueOrNull();
        
        sex = student.sex;
        birthday = student.birthday.MapToDto()!;
        isActive = student.isActive;
        
        religion = student.religion.GetValueOrDefault();
        diseases = student.diseases.GetValueOrDefault();
        address = student.address.GetValueOrDefault();
        
        file = student.file!;
        tutor = student.tutor!;
        parentList = student.parents;
        measurements = student.measurements!;
    }

    public StudentEntity ToEntity()
    {
        tutor.email = tutor.email.GetValueOrDefault();

        measurements ??= new StudentMeasurements();
        measurements.SetDefaultValues();
        
        return new StudentEntity
        {
            studentId = studentId,
            minedId = minedId,
            name = name,
            secondName = secondName,
            surname = surname,
            secondSurname = secondSurname,
            sex = sex,
            birthday = birthday.ToDateOnly(),
            isActive = isActive,
            religion = religion.GetValueOrDefault(),
            diseases = diseases.GetValueOrDefault(),
            address = address.GetValueOrDefault(),
            file = file,
            tutor = tutor,
            parents = parentList!.ToListEntity(),
            measurements = measurements,
            profilePicture = profilePicture
        };
    }
}