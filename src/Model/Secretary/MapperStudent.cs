using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;

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
            parentId = p.parentId,
            sex = p.sex,
            name = p.name,
            idCard = p.idCard,
            occupation = p.occupation
        }).ToList();
        
        while (parentsList.Count < 2)
        {
            parentsList.Add(new StudentParent
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
    
}