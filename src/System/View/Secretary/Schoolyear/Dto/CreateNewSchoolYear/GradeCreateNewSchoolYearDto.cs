namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class GradeCreateNewSchoolYearDto
{
    public string label { get; set; }
    public string schoolYear { get; set; }
    public string modality { get; set; }
    public string tag { get; set; }
    public List<SubjectsCreateNewSchoolYearDto> subjects { get; set; }

    public GradeCreateNewSchoolYearDto(DegreeDto degree)
    {
        label = degree.label;
        schoolYear = degree.schoolYear;
        modality = degree.modality;
        tag = degree.tag;
        subjects = degree.subjects.Select(SubjectDto.MapToSubjectsCreateNewSchoolYearDto).ToList();
    }
}