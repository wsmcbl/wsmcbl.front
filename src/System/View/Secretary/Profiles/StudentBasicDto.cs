using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.Profiles;

public class StudentBasicDto
{
    public string? studentId { get; set; }
    public string name { get; set; }
    public string? secondName { get; set; }
    public string surname { get; set; } 
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateEntity birthday { get; set; }

    public StudentBasicDto()
    {
        studentId = null;
        name = string.Empty;
        secondName = string.Empty;
        surname = string.Empty;
        secondSurname = string.Empty;
        ResetBirthDay();
    }

    public void checkData()
    {
        secondName = secondName?.Trim() == string.Empty ? null : secondName;
        secondSurname = secondSurname?.Trim() == string.Empty ? null : secondSurname;
    }
    
    public bool IsBirthdayValid() => !birthday.IsNotDateValid();

    public void ResetBirthDay()
    {
        birthday = new DateEntity(DateTime.Today);
    }
}