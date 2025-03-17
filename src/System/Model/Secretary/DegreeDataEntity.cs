namespace wsmcbl.src.Model.Secretary;

public class DegreeDataEntity
{
    public int degreeDataId { get; set; }
    public string label { get; set; } = null!;
    public string tag { get; set; } = null!;
    public int educationalLevel { get; set; }
    
    public List<SubjectDataEntity>? subjectList { get; private set; }

    public void SetSubjectList(List<SubjectDataEntity> list)
    {
        subjectList = list; 
    }
}