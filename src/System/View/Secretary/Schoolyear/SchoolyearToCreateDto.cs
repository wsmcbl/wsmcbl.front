namespace wsmcbl.src.View.Secretary.Schoolyear;

public class SchoolyearToCreateDto
{
    public List<PartialToCreateDto> partialList { get; set; }
    public List<TariffDto> tariffList { get; set; }

    public SchoolyearToCreateDto(List<TariffDto> tariffList, List<PartialToCreateDto> partialList)
    {
        this.tariffList = tariffList;
        this.partialList = partialList;
    }
}