namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class GradeCreateNewSchoolYearDto
{
    public string label { get; set; } = null!;
    public string schoolYear { get; set; } = null!;
    public string modality { get; set; } = null!;
    public List<SubjectsCreateNewSchoolYearDto> subjects { get; set; } = null!;
}