using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using wsmcbl.src.View.Secretary.SchoolYears;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public static class MapperStudent
{
    public static StudentEntity ToStudentEntity(this EnrollStudentDto dto)
    {
        return dto.GetStudentEntity();
    }
    public static List<StudentParent> ToListEntity(this List<StudentParent> list)
    {
        while (list.Count < 2)
        {
            var element = new StudentParent();
            element.init();
            list.Add(element);
        }

        return list;
    }

    public static EnrollStudentDto ToEnrollStudentDto(this StudentEntity student, string enrollmentId, int discountId, bool isRepeating)
    {
        return new EnrollStudentDto(student, enrollmentId, discountId, isRepeating);
    }
    
    public static StudentFullDto ToStudentFullDto(StudentEntity? entity)
    {
        return new()
        {
            studentId = entity!.studentId,
            minedId = entity.minedId,
            name = entity.name,
            secondName = entity.secondName,
            surname = entity.surname,
            secondSurname = entity.secondSurname,
            sex = entity.sex,
            birthday = entity.birthday.ToEntityOrNull()!,
            religion = entity.religion,
            diseases = entity.diseases,
            address = entity.address,
            isActive = entity.isActive,
            profilePicture = entity.profilePicture,
            tutor = entity.tutor,
            parentList = entity.parents,
            measurements = entity.measurements,
            file = entity.file
        };
    }
}