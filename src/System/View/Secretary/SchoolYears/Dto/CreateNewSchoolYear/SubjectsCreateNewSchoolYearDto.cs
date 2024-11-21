namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class SubjectsCreateNewSchoolYearDto
{
    public string name { get; set; } = null!;
    public bool isMandatory { get; set; }
    public int semester { get; set; }
    public string initials { get; set; } = null!;
    public int area { get; set; }
}