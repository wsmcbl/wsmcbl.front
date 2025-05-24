namespace wsmcbl.src.View.Academy.SubjectByTeacher;

public class SubjectByTeacherDto
{
    public string enrollmentId { get; set; } = "N/A";
    public string subjectId { get; set; } = "N/A";
    public int areaId { get; set; }
    public string name { get; set; } = "N/A";
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = "N/A";
    public int number { get; set; }
}