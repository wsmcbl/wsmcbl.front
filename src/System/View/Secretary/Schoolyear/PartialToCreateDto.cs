using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public class PartialToCreateDto
{
    public int partial { get; set; }
    public int semester { get; set; }
    public DateOnlyDto startDate { get; set; } = null!;
    public DateOnlyDto deadLine { get; set; } = null!;
}