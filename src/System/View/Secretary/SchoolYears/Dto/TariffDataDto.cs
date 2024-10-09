using Newtonsoft.Json;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.dto.Output;

public class TariffDataDto
{
    public string? concept { get; set; }
    public double amount { get; set; }
    public DateEntity DueDateEntity { get; set; }
    public int typeId { get; set; }
    public int modality { get; set; }
    
}