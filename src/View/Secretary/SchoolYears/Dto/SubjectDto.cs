using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class SubjectDto
{
    [JsonProperty("gradeIntId")] 
    public int GradeIntId { get; set; }
    
    [JsonProperty("name")] 
    public string Name { get; set; } = null!;
    
    [JsonProperty("isMandatory")] 
    public bool IsMandatory { get; set; }
    
    [JsonProperty("semester")] 
    public int Semester { get; set; }
    
    public string initials { get; set; }

    public static SubjectsCreateNewSchoolYearDto MapToSubjectsCreateNewSchoolYearDto(SubjectDto subjectDto)
    {
        return new SubjectsCreateNewSchoolYearDto()
        {
            name = subjectDto.Name,
            isMandatory = subjectDto.IsMandatory,
            semester = subjectDto.Semester,
            initials = subjectDto.initials
        };
    }
    
}