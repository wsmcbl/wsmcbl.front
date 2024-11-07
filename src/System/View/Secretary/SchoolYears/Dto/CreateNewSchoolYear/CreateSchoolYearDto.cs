using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;

public class CreateSchoolYearDto
{
    public double exchangeRate { get; set; }
    public List<GradeCreateNewSchoolYearDto>? degreeList { get; set; }
    public List<TariffCreateNewSchoolYearDto>? tariffList { get; set; }
    public List<PartialListDto> partialList { get; set; }

    public CreateSchoolYearDto()
    {
    }
    
    public CreateSchoolYearDto(SchoolYearEntity entity, List<PartialListDto> partials)
    {
        exchangeRate = entity.exchangeRate;
        degreeList = entity.degreeList.ToListDto();
        tariffList = entity.tariffList.ToListDto();
        partialList = partials;
    }
}