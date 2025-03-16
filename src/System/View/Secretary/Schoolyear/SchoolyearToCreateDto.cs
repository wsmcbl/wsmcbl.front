namespace wsmcbl.src.View.Secretary.Schoolyear;

public class CreateSchoolyearDto
{
    public List<PartialToCreateDto> partialList { get; set; } = null!;
    public List<TariffDto> tariffList { get; set; } = null!;
}