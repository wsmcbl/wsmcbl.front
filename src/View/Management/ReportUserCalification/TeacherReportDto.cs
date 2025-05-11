namespace wsmcbl.src.View.Management.ReportUserCalification;

public class TeacherReportDto
{
    public string name { get; set; } = null!;
    public bool hasSubmittedGrades { get; set; }
    public List<SubjectsReportDto> subjectList { get; set; } = [];
}
