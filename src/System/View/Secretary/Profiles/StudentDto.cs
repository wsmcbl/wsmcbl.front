using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.Profiles;
public class StudentDto
{
    public string? studentId { get; set; }
    public string name { get; set; } = null!;
    public string? secondName { get; set; }
    public string surname { get; set; } = null!;
    public string? secondSurname { get; set; }
    public bool sex { get; set; }
    public DateOnly birthday { get; set; }
    public TutorToCreateDto tutor { get; set; }
    public int educationalLevel { get; set; }

}