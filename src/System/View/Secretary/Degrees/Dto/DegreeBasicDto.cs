using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Secretary.Degrees.Dto;

public class DegreeBasicDto
{
    public string degreeId { get; set; } = null!;
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public int quantity { get; set; }
    public int Sections { get; set; }
    public string modality { get; set; } = null!;
    public List<EnrollmentBasicDto> enrollments {get; set;} = null!;
    public List<SubjectDto> subjects { get; set; } = null!;
    
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
    
    private List<TeacherEntity> teacherList = null!;
    public void SetTeacherList(List<TeacherEntity> list)
    {
        teacherList = list;
    }
}