using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.Profiles;
public class NewStudentDto
{
    public StudentBasicDto student { get; set; }
    public TutorToCreateDto tutor { get; set; }
    public int educationalLevel { get; set; }
}