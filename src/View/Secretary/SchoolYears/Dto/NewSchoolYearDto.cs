using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;
    
public class NewSchoolYearDto
{
    public List<GradeCreateNewSchoolYearDto>? degrees { get; set; }
    public List<SchoolYearTariffs>? tariffs { get; set; }
}