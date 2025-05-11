using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Components.CreateStudent;

public class StudentBasicDto
{
    public string? studentId { get; set; }
    public string name { get; set; }
    public string? secondName { get; set; }
    public string surname { get; set; } 
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnlyDto birthday { get; set; } = null!;

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
        birthday = new DateOnlyDto(DateTime.Today);
    }
}