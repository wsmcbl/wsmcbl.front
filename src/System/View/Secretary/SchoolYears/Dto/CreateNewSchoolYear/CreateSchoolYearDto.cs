namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class CreateSchoolYearDto
{
    public double exchangeRate { get; set; }
    public List<GradeCreateNewSchoolYearDto>? degrees { get; set; }
    public List<TariffCreateNewSchoolYearDto>? tariffs { get; set; }
    public List<PartialListDto> partialList { get; set; }
}