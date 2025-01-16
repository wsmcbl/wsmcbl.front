namespace wsmcbl.src.Model.Academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; } = null!;
    public string? teacherId { get; set; }
    public string label { get; set; } = null!;  
    public int capacity { get; set; }
    public int quantity { get; set; }
    public string? section { get; set; }
    public List<StudentEntity> studentList { get; set; } = new List<StudentEntity>();    
    public List<MinimalSubject> subjectList { get; set; } = new List<MinimalSubject>();
    public List<(SubjectEntity subject, TeacherEntity teacher)> SubjectTeacherList { get; private set; } = null!;
    
    public class MinimalSubject
    {
        public string? subjectId { get; set; }
        public string? teacherId { get; set; }
        public string? name { get; set; }
    }
    

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