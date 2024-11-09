namespace wsmcbl.src.View.Secretary.Profiles;

public class NewStudentDto
{
    public StudentBasicDto student { get; set; }
    public TutorToCreateDto tutor { get; set; }
    public int educationalLevel { get; set; }

     public bool IsNameValid()
     {
         return !string.IsNullOrWhiteSpace(student.name) && !string.IsNullOrWhiteSpace(student.surname);
     }

    
     
     public bool IsTutorValid() => tutor.IsTutorValid();
     public bool IsBirthdayValid() => student.IsBirthdayValid();



}