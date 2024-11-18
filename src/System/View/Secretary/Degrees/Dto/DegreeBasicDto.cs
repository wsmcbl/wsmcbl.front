using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class DegreeBasicDto
{
    public string degreeId { get; set; }
    public string label { get; set; }
    public string schoolYear { get; set; }
    public int quantity { get; set; }
    public int Sections { get; set; }
    public string modality { get; set; }
    public List<EnrollmentBasicDto> enrollments {get; set;}
    public List<SubjectDto> subjects { get; set; }
    
    public DegreeEntity toEntity()
    {
        var degree = new DegreeEntity
        {
            degreeId = degreeId,
            label = label,
            schoolYear = schoolYear,
            quantity = quantity,
            sections = Sections,
            educationalLevel = modality,
            EnrollmentList = [],
            SubjectList = []
        };
        
        foreach (var item in subjects)
        {
            degree.SubjectList.Add(item.ToEntity());
        }

        foreach (var item in enrollments)
        {
            var entity = item.ToEntity(degree.SubjectList, teacherList!);
            degree.EnrollmentList.Add(entity);
        }
        
        return degree;
    }
    
    private List<TeacherEntity>? teacherList;
    public void SetTeacherList(List<TeacherEntity> teacherList)
    {
        this.teacherList = teacherList;
    }
}