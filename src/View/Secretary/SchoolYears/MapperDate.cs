using wsmcbl.src.dto.Output;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public static class MapperDate
{
    public static TariffDataDto MapToTariffDataDto(SchoolYearTariffs schoolYearTariffs)
    {
        return new TariffDataDto
        {
            concept = schoolYearTariffs.Concept,
            amount = schoolYearTariffs.Amount,
            DueDateClass = ConvertDateOnlyToDate(schoolYearTariffs.OnlyDate),
            typeId = schoolYearTariffs.Type,
            modality = schoolYearTariffs.Modality
        };
    }
    
    public static DateClass? ConvertDateOnlyToDate(DateOnly? dateOnly)
    {
        if (dateOnly.HasValue)
        {
            if (dateOnly.Value.Day == 1 && dateOnly.Value.Month == 1 && dateOnly.Value.Year == 1)
            {
                return null;
            }
            
            return new DateClass
            {
                year = dateOnly.Value.Year,
                month = dateOnly.Value.Month,
                day = dateOnly.Value.Day
            };
        }
        return null;
    }
    
    public static DateOnly DateClassToDateOnly(DateClass date)
    {
        return new DateOnly(date.year, date.month, date.day);
    }
    
    public static DateOnly DateClassToDateNow(DateClass date)
    {
        return new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }
}