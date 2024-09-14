namespace wsmcbl.src.Model.Academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; }
    public string? teacherId { get; set; }
    public string Label { get; set; } = null!;
    public int Capacity { get; set; }
    public int Quantity { get; set; }
    public string Section { get; set; }
    
    public List<StudentEntity> Students { get; set; } = null!;
    
    public List<(SubjectEntity subject, TeacherEntity? teacher)> SubjectTeacherList { get; set; }

    public void SetSubjectTeacherList(List<SubjectEntity> list)
    {
        SubjectTeacherList = [];
        var nullTeacher = new NullTeacherEntity();
        foreach (var item in list)
        {
            SubjectTeacherList.Add((item, nullTeacher));
        }
    }
}