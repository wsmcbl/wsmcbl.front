using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;
using TariffCreateNewSchoolYearDto = wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear.TariffCreateNewSchoolYearDto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public static class MapperSchoolYear
{
    public static List<GradeCreateNewSchoolYearDto> ToListDto(this List<DegreeDto>? listGrade)
    {
        return listGrade == null ? [] : 
            listGrade.Select(e => new GradeCreateNewSchoolYearDto(e)).ToList();
    }
    
    public static List<TariffCreateNewSchoolYearDto> ToListDto(this List<SchoolyearTariffDto> tariffList)
    {
        return tariffList.Select(e => new TariffCreateNewSchoolYearDto(e)).ToList();
    }
    
}