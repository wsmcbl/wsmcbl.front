using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

public class BasicSchoolyearDto
{ 
    public string schoolyearId { get; set; } = null!;
    public string label { get; set; } = null!;
    public DateOnlyDto startDate { get; set; } = null!;
    public DateOnlyDto deadLine { get; set; } = null!; 
    public bool isActive { get; set; }
}