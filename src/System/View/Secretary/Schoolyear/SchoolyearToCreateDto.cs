using wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public class SchoolyearToCreateDto
{
    public List<PartialsDto> partialList { get; set; }
    public List<TariffDto> tariffList { get; set; }

    public SchoolyearToCreateDto(List<TariffDto> tariffList, List<PartialsDto> partialList)
    {
        this.tariffList = tariffList;
        this.partialList = partialList;
    }
}