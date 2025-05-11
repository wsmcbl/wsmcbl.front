namespace wsmcbl.src.View.Secretary.Schoolyear;

public class SubjectDto
{
    public int areaId { get; set; }
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;
    public int number { get; set; }
}