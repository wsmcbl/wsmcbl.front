namespace wsmcbl.src.Model.Academy;

public class DegreeEntity
{
    public string DegreeId { get; set; }
    public string Label { get; set; } = null!;
    public string SchoolYear { get; set; } = null!;
    public int Quantity { get; set; }

    public int Sections { get; set; }
    public string Modality { get; set; } = null!;
    public List<EnrollmentEntity>? EnrollmentList { get; set; } = null!;
    public List<SubjectEntity> SubjectList { get; set; }
}