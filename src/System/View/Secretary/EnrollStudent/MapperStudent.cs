using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

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
            birthday = dto.birthday.ToDateOnly(),
            religion = dto.religion,
            diseases = dto.diseases,
            address = dto.address,
            isActive = dto.isActive,
            file = dto.file,
            tutor = dto.tutor,
            parents = getParents(dto.parents),
            measurements = dto.measurements
        };
    }
    private static List<StudentParent> getParents(List<StudentParent> list)
    
    {
        while (list.Count < 2)
        {
            var element = new StudentParent();
            element.init();
            list.Add(element);
        }

        return list;
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
                birthday = student.birthday.ToEntityOrNull(),
                religion = student.religion,
                diseases = student.diseases,
                address = student.address,
                isActive = student.isActive,
                file = student.file,
                tutor = student.tutor,
                parents = student.parents,
                measurements = student.measurements
            }
        };
    }
 
    
}