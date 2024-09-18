using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;
using File = wsmcbl.src.View.Secretary.EnrollmentStudent.Dto.File;

namespace wsmcbl.src.Model.Secretary;

public static class MapperStudent
{
    public static StudentEntity MapToEntity(StudentFullDto dto)
    {
        return new StudentEntity
        {
            studentId = dto.studentId,
            name = dto.name,
            secondName = dto.secondName,
            surname = dto.surname,
            secondSurname = dto.secondSurname,
            sex = dto.sex,
            birthday = MapperDate.DateClassToDateOnly(dto.birthday),
            religion = dto.religion,
            diseases = dto.diseases,
            address = dto.address,
            isActive = dto.isActive,
            file = ToFile(dto),
            tutor = ToTutor(dto),
            parents = ToParent(dto),
            measurements = ToMeasurements(dto)
        };
    }
    private static StudentMeasurements ToMeasurements(StudentFullDto dto)
    {
        return new StudentMeasurements
        {
            measurementId = dto.measurements.measurementId,
            height = dto.measurements.height,
            weight = dto.measurements.weight,
        };
    }
    private static List<StudentParent> ToParent(StudentFullDto dto)
    {
        var parentsList = dto.parents.Select(p => new StudentParent
        {
            parentId = "",
            sex = p.sex,
            name = p.name,
            idCard = p.idCard,
            occupation = p.occupation
        }).ToList();
        
        while (parentsList.Count < 2)
        {
            parentsList.Add(new StudentParent
            {
                parentId = "",    
                sex = false,               
                name = "Sin asignar",          
                idCard = "Sin agignar",    
                occupation = "Sin asignar" 
            });
        }
        return parentsList.Take(2).ToList();
    }
    private static StudentTutor ToTutor(StudentFullDto dto)
    {
        return new StudentTutor
        {
            tutorId = dto.tutor.tutorId,
            name = dto.tutor.name,
            phone = dto.tutor.phone,
        };
    }
    private static StudentFile ToFile(StudentFullDto dto)
    {
        return new StudentFile
        {
            fileId = dto.file.fileId,
            transferSheet = dto.file.transferSheet,
            birthDocument = dto.file.birthDocument,
            parentIdentifier = dto.file.parentIdentifier,
            updatedGradeReport = dto.file.updatedGradeReport,
            conductDocument = dto.file.conductDocument,
            financialSolvency = dto.file.financialSolvency
        };
    }

    public static StudentEnrollmentDto MapToEnrollmentDto(StudentEntity student, string enrollmentId)
    {
        return new StudentEnrollmentDto
        {
            enrollmentId = enrollmentId,
            student = new StudentFullDto
            {
                studentId   = student.studentId,
                name = student.name,
                secondName = student.secondName,
                surname = student.surname,
                secondSurname = student.secondSurname,
                sex = student.sex,
                birthday = MapperDate.ConvertDateOnlyToDate(student.birthday),
                religion = student.religion,
                diseases = student.diseases,
                address = student.address,
                isActive = student.isActive,
                file = ToDtoFile(student),
                tutor = ToDtoTutor(student),
                parents = ToDtoParent(student),
                measurements = ToDtoMeasurements(student)
            }
        };
    }
    private static File ToDtoFile(StudentEntity entity)
    {
        return new File
        {
            fileId = entity.file.fileId,
            transferSheet = entity.file.transferSheet,
            birthDocument = entity.file.birthDocument,
            parentIdentifier = entity.file.parentIdentifier,
            updatedGradeReport = entity.file.updatedGradeReport,
            conductDocument = entity.file.conductDocument,
            financialSolvency = entity.file.financialSolvency
        };
    }
    private static Tutor ToDtoTutor(StudentEntity entity)
    {
        return new Tutor
        {
            tutorId = entity.tutor.tutorId,
            name = entity.tutor.name,
            phone = entity.tutor.phone,
        };
    }
    private static List<Parent> ToDtoParent(StudentEntity entity)
    {
        var parentsList = entity.parents.Select(p => new Parent
        {
            parentId = p.parentId,
            sex = p.sex,
            name = p.name,
            idCard = p.idCard,
            occupation = p.occupation
        }).ToList();
        
        while (parentsList.Count < 2)
        {
            parentsList.Add(new Parent
            {
                parentId = "defaultId",    
                sex = false,               
                name = "Sin asignar",          
                idCard = "Sin agignar",    
                occupation = "Sin asignar" 
            });
        }
        return parentsList.Take(2).ToList();
    }
    private static Measurements ToDtoMeasurements(StudentEntity entity)
    {
        return new Measurements
        {
            measurementId = entity.measurements.measurementId,
            height = entity.measurements.height,
            weight = entity.measurements.weight,
        };
    }
    
}