namespace wsmcbl.src.View.Secretary.Schoolyear.SchoolYearView.New;

public class CreateSchoolYearDto
{
    public List<PartialsDto> partialList { get; set; } = new();
    public List<TariffDto> tariffList { get; set; } = new();
}