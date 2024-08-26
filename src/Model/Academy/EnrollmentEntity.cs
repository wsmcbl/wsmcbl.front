namespace wsmcbl.src.Model.Academy;

public class EnrollmentEntity
{
    public string EnrollmentId { get; set; }
    public string? TeacherId { get; set; }
    public string Label { get; set; } = null!;
    public string SchoolYear { get; set; } = null!;
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