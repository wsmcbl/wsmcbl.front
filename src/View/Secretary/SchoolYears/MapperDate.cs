using wsmcbl.front.dto.Output;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public static class MapperDate
{
    public static TariffDataDto MapToTariffDataDto(SchoolYearTariffs schoolYearTariffs)
    {
        return new TariffDataDto
        {
            Concept = schoolYearTariffs.Concept,
            Amount = schoolYearTariffs.Amount,
            dueDate = ConvertDateOnlyToDate(schoolYearTariffs.OnlyDate),
            Type = schoolYearTariffs.Type,
            Modality = schoolYearTariffs.Modality
        };
    }
    
    private static Date? ConvertDateOnlyToDate(DateOnly? dateOnly)
    {
        if (dateOnly.HasValue)
        {
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