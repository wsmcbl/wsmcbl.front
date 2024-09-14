namespace wsmcbl.src.Model.Academy;

public class DegreeEntity
{
    public string degreeId { get; set; }
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public int quantity { get; set; }
    public int sections { get; set; }
    public string modality { get; set; } = null!;
    public List<EnrollmentEntity>? EnrollmentList { get; set; } = null!;
    public List<SubjectEntity> SubjectList { get; set; }
}