namespace wsmcbl.src.View.Academy.EnrollmentListByTeacher;

public class EnrollmentByTeacherDto
{
    public string degreeId { get; set; } = null!;
    public string enrollmentId { get; set; } = null!;
    public string enrollmentLabel { get; set; } = null!;
    public int position { get; set; }
    public int studentQuantity {get; set;}
    public int subjectQuantity { get; set; }
}