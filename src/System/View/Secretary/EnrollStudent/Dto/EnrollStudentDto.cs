using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.EnrollStudent.Dto;

public class EnrollStudentDto
{
    public string? enrollmentId { get; set; }
    public int discountId { get; set; }
    public StudentFullDto student { get; set; }

    public EnrollStudentDto()
    {
    }
    
    public EnrollStudentDto(StudentEntity student, string enrollmentId, int discountId)
    {
        this.enrollmentId = enrollmentId;
        this.discountId = discountId;
        this.student = new StudentFullDto(student);
    }

    public StudentEntity GetStudentEntity()
    {
        return student.ToEntity();
    }
}
