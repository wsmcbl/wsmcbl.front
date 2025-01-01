using wsmcbl.src.View.Academy.AddGrade;

namespace wsmcbl.src.Model.Academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public string schoolyear { get; set; } = null!;
    public string enrollment { get; set; } = null!;
    public int conductgrade { get; set; }
    
    public List<GradeEntity>? gradeList {get; set;}


    public double getCoductGrade()
    {
        if (gradeList == null || gradeList.Count == 0)
        {
            return 0;
        }
        
        return gradeList[0].grade;
    }

    public void setConductGrade(int conduct)
    {
        foreach (var item in gradeList!)
        {
            item.conductGrade = conduct;
        }
    }
}