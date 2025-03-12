namespace wsmcbl.src.View.Academy.EnrollmentGuide;

public class EnrollmentDto
{
    public string enrollmentId { get; set; } = "N/A";
    public string label { get; set; } = "N/A";
    public string section { get; set; } = "N/A";
    public int capacity { get; set; }
    public int quantity { get; set; }
    public string schoolYear { get; set; } = "N/A";
    public List<StudentDto> studentList { get; set; } = new List<StudentDto>();
    public List<SubjectDto> subjectList { get; set; } = new List<SubjectDto>();
    public List<SubjectTeacherDto> subjectTeacherIdList { get; set; } = new List<SubjectTeacherDto>();
}