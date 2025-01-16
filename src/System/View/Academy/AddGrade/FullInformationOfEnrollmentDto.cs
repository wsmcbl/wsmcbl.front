using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.AddGrade;

public class FullInformationOfEnrollmentDto
{
    public string label { get; set; } = null!;
    public List<StudentEntity> studentList { get; set; } = null!;
    public List<SubjectEntity> subjectList { get; set; } = null!;
    public List<SubjectWithGradeListDto> subjectPartialList { get; set; } = null!;

    public void deleteWithoutGrades()
    {
        subjectList = subjectList.Where(e => hasGrades(e)).ToList();
    }

    private bool hasGrades(SubjectEntity value)
    {
        return subjectPartialList.FirstOrDefault(e => e.subjectId == value.subjectId) != null;
    }


    private List<GradeEntity> getGradeListByStudent(string studentId)
    {
        var gradeList = new List<GradeEntity>();
        foreach (var item in subjectPartialList)
        {
            var grade = item.grades!.FirstOrDefault(e => e.studentId == studentId);
            grade!.subjectId = item.subjectId;
            gradeList.Add(grade);
        }
        
        return gradeList;
    }

    public void updateStudentGradeList()
    {
        foreach (var item in studentList)
        {
            item.gradeList = getGradeListByStudent(item.studentId);
        }
    }
}