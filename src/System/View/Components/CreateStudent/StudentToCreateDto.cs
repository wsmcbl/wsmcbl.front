namespace wsmcbl.src.View.Components.CreateStudent;

public class StudentToCreateDto
{
    public StudentBasicDto student { get; set; } = null!;
    public TutorToCreateDto tutor { get; set; } = null!;
    public int educationalLevel { get; set; }

    
    public void SetAsDefault()
    {
        student = new StudentBasicDto();
        student.ResetBirthDay();
        
        tutor = new TutorToCreateDto();
        educationalLevel = 1;
    }
    
     public bool IsNameValid()
     {
         return !string.IsNullOrWhiteSpace(student.name) && !string.IsNullOrWhiteSpace(student.surname);
     }

     public void checkData()
     {
         student.checkData();
     }
     
     public bool IsTutorValid() => tutor.IsTutorValid();
     public bool IsBirthdayValid() => student.IsBirthdayValid();
}