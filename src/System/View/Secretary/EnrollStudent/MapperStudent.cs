using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

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
 
    
}