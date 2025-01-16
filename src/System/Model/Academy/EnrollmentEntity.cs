namespace wsmcbl.src.Model.Academy;

public class EnrollmentEntity
{
    public string enrollmentId { get; set; } = null!;
    public string? teacherId { get; set; }
    public string label { get; set; } = null!;
    public int capacity { get; set; }
    public int quantity { get; set; }
    public string? section { get; set; }
    public List<StudentEntity> studentList { get; set; } = [];
    public List<SubjectEntity> subjectList { get; set; } = [];

    public void updateSubjectList(List<SubjectEntity> list)
    {
        foreach (var item in subjectList)
        {
            var result = list.FirstOrDefault(e => e.subjectId == item.subjectId);
            if (result == null)
            {
                continue;
            }

            item.updateData(result);
        }
    }
}