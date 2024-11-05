using Newtonsoft.Json;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto;

public class SchoolYearTariffs
{ 
    public string? schoolYear { get; set; } 
    public string? concept { get; set; }
    public double amount { get; set; }
    public DateEntity? dueDate { get; set; }
    public DateOnly OnlyDate { get; set; }
    public int type { get; set; }
    public int modality { get; set; }
}