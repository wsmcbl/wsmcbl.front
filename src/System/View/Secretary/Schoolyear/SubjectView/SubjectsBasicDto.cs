namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView;

public class SubjectsBasicDto
{
    public int subjectDataId { get; set; }
    public int areaId { get; set; }
    public int degreeDataId { get; set; }
    public string name { get; set; } = "N/A";
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = "N/A";
    public int number { get; set; }
    public bool isActive { get; set; }
}