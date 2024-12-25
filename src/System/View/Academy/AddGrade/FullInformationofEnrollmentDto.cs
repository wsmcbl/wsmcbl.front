namespace wsmcbl.src.View.Academy.AddGrade;

public class FullInformationofEnrollmentDto
{
    public string label { get; set; } = null!;
    public List<StudentDto> studentList { get; set; } = null!;
    public List<SubjectsDto> subjectList { get; set; } = null!;
    public List<GradesOfEnrollmentsDto> subjectPartialList { get; set; } = null!;
}