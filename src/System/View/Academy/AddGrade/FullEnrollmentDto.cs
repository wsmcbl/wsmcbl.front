using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.AddGrade;

public class FullEnrollmentDto
{
    public string label { get; set; } = null!;
    public List<StudentEntity> studentList { get; set; } = null!;
    public List<SubjectEntity> subjectList { get; set; } = null!;
    public List<SubjectWithGradeListDto> subjectPartialList { get; set; } = null!;

    public void Init()
    {
        label = string.Empty;
        studentList = [];
        subjectList = [];
        subjectPartialList = [];
    }

    public void DeleteWithoutGrades()
    {
        subjectList = subjectList.Where(e => HasGrades(e)).ToList();
    }

    private bool HasGrades(SubjectEntity value)
    {
        return subjectPartialList.FirstOrDefault(e => e.subjectId == value.subjectId) != null;
    }

    public void UpdateStudentGradeList()
    {
        foreach (var item in studentList)
        {
            item.gradeList = GetGradeListByStudent(item.studentId);
            item.LoadConductGradeFromList();
        }
    }
    
    private List<GradeEntity> GetGradeListByStudent(string studentId)
    {
        var gradeList = new List<GradeEntity>();
        foreach (var item in subjectPartialList)
        {
            var grade = item.grades!.FirstOrDefault(e => e.studentId == studentId);
            if (grade == null)
            {
                continue;
            }
            
            grade.subjectId = item.subjectId;
            gradeList.Add(grade);
        }
        
        return gradeList;
    }
}