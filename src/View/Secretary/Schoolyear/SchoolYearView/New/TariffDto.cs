using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;

public class TariffDto
{
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public int typeId { get; set; }
    public int educationalLevel { get; set; }    
}