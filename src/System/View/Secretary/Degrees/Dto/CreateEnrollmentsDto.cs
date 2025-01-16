using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class CreateEnrollmentsDto
{
    public string enrollmentId { get; set; } = null!;
    public string? teacherId { get; set; }
    public string? section { get; set; }
    public string? label { get; set; }
    public int capacity { get; set; }
    public int quantity { get; set; }
    public List<SubjectTeacherDto> subjectList { get; set; } = null!;
 

    public CreateEnrollmentsDto(EnrollmentEntity enrollment)
    {
        enrollmentId = enrollment.enrollmentId;
        teacherId = enrollment.teacherId!;
        section = enrollment.Section!;
        label = enrollment.Label;
        capacity = enrollment.Capacity;
        quantity = enrollment.Quantity;
        subjectList = enrollment.MapToListDto();
    }
}