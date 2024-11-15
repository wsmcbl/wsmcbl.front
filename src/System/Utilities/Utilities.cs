using System.Globalization;

namespace wsmcbl.src.Utilities;

public static class Utilities
{
    public static string TokenKey => "authToken";
    public static string ToStringFormat(this DateOnly? datetime)
    {
        if (datetime == null)
            return string.Empty;
        
        var culture = new CultureInfo("es-ES")
        {
            DateTimeFormat =
            {
                AMDesignator = "AM",
                PMDesignator = "PM"
            }
        };

        return ((DateOnly)datetime).ToString("dd/MMM/yyyy", culture);
    }
}