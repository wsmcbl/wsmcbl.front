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
            dueDate = ConvertDateOnlyToDate(schoolYearTariffs.OnlyDate),
            typeId = schoolYearTariffs.Type,
            modality = schoolYearTariffs.Modality
        };
    }
    
    private static Date? ConvertDateOnlyToDate(DateOnly? dateOnly)
    {
        if (dateOnly.HasValue)
        {
            if (dateOnly.Value.Day == 1 && dateOnly.Value.Month == 1 && dateOnly.Value.Year == 1)
            {
                return null;
            }
            return new Date
            {
                Year = dateOnly.Value.Year,
                Month = dateOnly.Value.Month,
                Day = dateOnly.Value.Day
            };
        }
        return null;
    }
    
    
}