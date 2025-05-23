using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

public class CreateTariffDto
{
    public int tariffDataId { get; set; }
    public string concept { get; set; } = null!;
    public decimal amount { get; set; }
    public DateOnlyDto? dueDate { get; set; }
    public int typeId { get; set; }
    public int educationalLevel { get; set; }
    public bool isActive { get; set; }
}