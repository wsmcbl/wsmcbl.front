using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.AddGrade;

public class SubjectWithGradeListDto
{
    public int partialId { get; set; }
    public string subjectId { get; set; } = null!;
    public List<GradeEntity>? grades { get; set; }
}