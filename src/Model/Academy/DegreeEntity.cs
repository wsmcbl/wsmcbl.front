namespace wsmcbl.src.Model.Academy;

public class DegreeEntity
{
    public string? degreeId { get; set; }
    public string label { get; set; } = "N/A";
    public string schoolYear { get; set; } = "N/A";
    public int quantity { get; set; }
    public string tag { get; set; } = "N/A";
    public int position { get; set; }
    public string educationalLevel { get; set; } = "N/A";
    public List<EnrollmentEntity> EnrollmentList { get; set; } = new();
    public List<SubjectEntity> SubjectList { get; set; } = new();

    public bool HaveEnrollments()
    {
        return quantity < 1;
    }
}