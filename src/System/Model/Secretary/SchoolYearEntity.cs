using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.Model.Secretary;
public class SchoolYearEntity
{
    public string? id { get; set; }
    public string? label { get; set; }
    public string? startDate { get; set; }
    public string? deadLine { get; set; }
    public bool isActive { get; set; }
    public double exchangeRate { get; set; }
    public List<GradeDto>? degreeList { get; set; }
    public List<SchoolYearTariffs>? tariffList { get; set; }
}