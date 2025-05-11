namespace wsmcbl.src.Model.Academy;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public bool sex { get; set; }
    public string schoolyear { get; set; } = null!;
    public string enrollment { get; set; } = null!;
    public decimal conductGrade { get; set; }
    
    public List<GradeEntity>? gradeList {get; set;}


    public void LoadConductGradeFromList()
    {
        if (gradeList == null || gradeList.Count == 0)
        {
            conductGrade = 0;
            return;
        }
        
        conductGrade = gradeList[0].conductGrade;
    }
    public void UpdateConductGradeIntoList()
    {
        foreach (var item in gradeList!)
        {
            item.conductGrade = conductGrade;
        }
    }
    public bool HasAnyZeroGrade()
    {
        return gradeList == null || 
               gradeList.Count == 0 || 
               gradeList.Any(g => g.grade == 0);
    }
}