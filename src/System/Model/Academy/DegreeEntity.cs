namespace wsmcbl.src.Model.Academy;

public class DegreeEntity
{
    public string? degreeId { get; set; }
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public int quantity { get; set; }
    public int sections { get; set; }
    public int position { get; set; }
    public string? educationalLevel { get; set; } 
    public List<EnrollmentEntity>? EnrollmentList { get; set; }
    public List<SubjectEntity>? SubjectList { get; set; }

    public bool haveEnrollments()
    {
        return quantity < 1;
    }
}