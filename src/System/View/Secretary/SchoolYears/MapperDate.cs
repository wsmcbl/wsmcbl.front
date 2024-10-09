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
            DueDateEntity = ConvertDateOnlyToDate(schoolYearTariffs.OnlyDate),
            typeId = schoolYearTariffs.Type,
            modality = schoolYearTariffs.Modality
        };
    }
    
    public static DateEntity? ConvertDateOnlyToDate(DateOnly? dateOnly)
    {
        if (dateOnly.HasValue)
        {
            if (dateOnly.Value.Day == 1 && dateOnly.Value.Month == 1 && dateOnly.Value.Year == 1)
            {
                return null;
            }
            
            return new DateEntity
            {
                year = dateOnly.Value.Year,
                month = dateOnly.Value.Month,
                day = dateOnly.Value.Day
            };
        }
        return null;
    }
    
    public static DateOnly DateClassToDateOnly(DateEntity date)
    {
        return new DateOnly(date.year, date.month, date.day);
    }
    
    public static DateOnly DateClassToDateNow(DateEntity date)
    {
        return new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }
    
    public static int? CalcularEdad(DateOnly? fechaNacimiento)
    {
        if (fechaNacimiento == null)
        {
            return null; 
        }

        DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Now);
        int edad = fechaActual.Year - fechaNacimiento.Value.Year;

        if (fechaActual < fechaNacimiento.Value.AddYears(edad))
        {
            edad--;
        }

        return edad;
    }


    
}