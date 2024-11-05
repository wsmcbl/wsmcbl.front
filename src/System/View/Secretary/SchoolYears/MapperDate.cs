using wsmcbl.src.dto.Output;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public static class MapperDate
{
    public static TariffDataDto MapToTariffDataDto(SchoolYearTariffs schoolYearTariffs)
    {
        return new TariffDataDto
        {
            concept = schoolYearTariffs.concept,
            amount = schoolYearTariffs.amount,
            DueDateEntity = schoolYearTariffs.OnlyDate.ToEntity(),
            typeId = schoolYearTariffs.type,
            modality = schoolYearTariffs.modality
        };
    }
    
    public static DateEntity? ToEntityOrNull(this DateOnly? dateOnly)
    {
        return dateOnly == null ? null : ToEntity((DateOnly)dateOnly);
    }

    public static DateEntity ToEntity(this DateOnly dateOnly) => new(dateOnly);

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
