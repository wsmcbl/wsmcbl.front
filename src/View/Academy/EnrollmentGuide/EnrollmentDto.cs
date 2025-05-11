using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.EnrollmentGuide;

public class EnrollmentDto
{
    public string enrollmentId { get; set; } = "N/A";
    public string label { get; set; } = "N/A";
    public string section { get; set; } = "N/A";
    public int capacity { get; set; }
    public int quantity { get; set; }
    public string schoolYear { get; set; } = "N/A";
    public List<StudentDto> studentList { get; set; } = [];
    public List<SubjectEntity> subjectList { get; set; } = [];
    public List<SubjectTeacherDto> subjectTeacherIdList { get; set; } = [];
    
    public void OrderSubjectList()
    {
        subjectList = subjectList.OrderBy(e => e.areaId).ThenBy(e => e.number).ToList();

        var list = subjectList.Select(e => e.subjectId).ToList();
        subjectTeacherIdList = subjectTeacherIdList.OrderBy(e => list.IndexOf(e.subjectId)).ToList();
    }

    public void OrderStudentList()
    {
        studentList = studentList.OrderBy(s => s.sex).ThenBy(s => s.fullName).ToList();
    }
}