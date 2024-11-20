namespace wsmcbl.src.Model.Academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; } = null!;
    public string? teacherId { get; set; }
    public string Label { get; set; } = null!;
    public int Capacity { get; set; }
    public int Quantity { get; set; }
    public string? Section { get; set; }
    
    public List<(SubjectEntity subject, TeacherEntity teacher)> SubjectTeacherList { get; private set; } = null!;

    public void SetSubjectTeacherList(List<(SubjectEntity subject, TeacherEntity teacher)> list)
    {
        SubjectTeacherList = list; 
    }

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