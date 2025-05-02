namespace wsmcbl.src.Model.Academy;

public class SubjectEntity
{
    public string subjectId { get; set; } = "N/A";
    public string teacherId { get; set; } = "N/A";
    public string initials { get; set; } = "N/A";
    public string name { get; set; } = "N/A";
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public int areaId { get; set; }
    public int number { get; set; }

    public void UpdateData(SubjectEntity subject)
    {
        initials = subject.initials;
        name = subject.name;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
    }
}