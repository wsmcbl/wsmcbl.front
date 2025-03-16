using wsmcbl.src.dto.Output;
using wsmcbl.src.View.Components.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;
using wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear;
using TariffCreateNewSchoolYearDto = wsmcbl.src.View.Secretary.SchoolYears.Dto.CreateNewSchoolYear.TariffCreateNewSchoolYearDto;

namespace wsmcbl.src.View.Secretary.Schoolyear;

public static class DtoMapper
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
    
    public static TariffDataDto MapToTariffDataDto(SchoolyearTariffDto schoolyearTariffDto)
    {
        return new TariffDataDto
        {
            concept = schoolyearTariffDto.concept,
            amount = schoolyearTariffDto.amount,
            DueDateEntity = schoolyearTariffDto.OnlyDate.ToEntity(),
            typeId = schoolyearTariffDto.type,
            modality = schoolyearTariffDto.modality
        };
    }
    
    public static int AgeCompute(this DateOnly? birthday)
    {
        if (birthday == null)
        {
            return 0;
        }

        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthday.Value.Year;

        if (today < birthday.Value.AddYears(age))
        {
            age--;
        }

        return age;
    }
}