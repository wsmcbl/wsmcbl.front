using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Model.Secretary;
public class SchoolYearEntity
{
    public string? SchoolYearId { get; set; }
    public string? Label { get; set; }
    public string? StartDate { get; set; }
    public string? DeadLine { get; set; }
    public bool IsActive { get; set; }
    public List<GradeDto>? Degrees { get; set; }
    public List<SchoolYearTariffs>? Tariffs { get; set; }
}