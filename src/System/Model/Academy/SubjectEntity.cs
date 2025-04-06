namespace wsmcbl.src.Model.Academy;

public class SubjectEntity
{
    public string? subjectId { get; set; }
    public string? teacherId { get; set; }
    public string? initials { get; set; }
    public string? name { get; set; } 
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public int areaId { get; set; }
    public int number { get; set; }

    public void updateData(SubjectEntity subject)
    {
        initials = subject.initials;
        name = subject.name;
        isMandatory = subject.isMandatory;
        semester = subject.semester;
    }
}