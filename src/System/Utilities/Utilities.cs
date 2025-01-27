using System.Globalization;

namespace wsmcbl.src.Utilities;

public static class Utilities
{
    public static string TokenKey => "authToken";
    
    public static string ToStringFormat(this DateOnly? date)
    {
        if (date == null)
        {
            return string.Empty;
        }

        var culture = new CultureInfo("es-ES");
        return ((DateOnly)date).ToString("dd/MMM/yyyy", culture);
    }

    private static string ToStringFormat(this DateTime dateTime)
    {
        var culture = new CultureInfo("es-ES")
        {
            DateTimeFormat =
            {
                AMDesignator = "am",
                PMDesignator = "pm"
            }
        };

        return dateTime.ToString("dddd dd/MMM/yyyy, h:mm tt", culture);   
    }
    
    public static string ToDateTimeFormat(this string date)
    {
        var result = DateTime.TryParse(date, out var value);
        return result ? value.ToStringFormat() : "Sin fecha.";
    } 
    
    public static string ToStringValid(this bool isValid) => isValid ? "Válido" : "Inválido";
    
    public static string ToStringYesOrNo(this bool value) => value ? "Sí" : "No";

    public static string? GetValueOrNull(this string? value) => string.IsNullOrWhiteSpace(value?.Trim()) ? null : value;
    
    public static string GetValueOrDefault(this string? value) => string.IsNullOrWhiteSpace(value) ? "N/A" : value;
}