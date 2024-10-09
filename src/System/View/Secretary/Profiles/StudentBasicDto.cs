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
        studentId = "";
        name = "Sin asignar";
        secondName = "Sin asignar";
        surname = "Sin asignar";
        secondSurname = "Sin asignar";
        birthday = new DateEntity(DateTime.Today);
    }
}