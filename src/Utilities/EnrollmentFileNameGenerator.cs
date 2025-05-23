namespace wsmcbl.src.Utilities;

public class EnrollmentFileNameGenerator
{
    private readonly string? schoolyear;
    private readonly string? enrollmentAbbreviation;

    public EnrollmentFileNameGenerator(string enrollmentLabel, string schoolyear)
    {
        enrollmentAbbreviation = getEnrollmentAbbreviation(enrollmentLabel);
        this.schoolyear = schoolyear;
    }

    private static string getEnrollmentAbbreviation(string value)
    {
        var list = value.Split(' ');
        return list.Length < 3 ? "0A" : $"{getDegreeNumber(list[0], list[1])}{list[2]}";
    }

    private static string getDegreeNumber(string value, string educationalLevel)
    {
        if (educationalLevel.Equals("Nivel"))
        {
            return value switch
            {
                "Primer" => "I",
                "Segundo" => "II",
                "Tercero" => "III",
                _ => "0"
            };    
        }
        
        return (value switch
        {
            "Primero" => 1,
            "Segundo" => 2,
            "Tercero" => 3,
            "Cuarto" => 4,
            "Quinto" => 5,
            "Sexto" => 6,
            "Septimo" => 7,
            "Octavo" => 8,
            "Noveno" => 9,
            "Décimo" => 10,
            "Undécimo" => 11,
            _ => 0
        }).ToString();
    }

    public string GetFileName(string partialLabel, string type)
    {
        var now = DateTime.UtcNow.toUTC6();
        var date = now.ToString("yyyyMMdd"); // 20250405
        var time = now.ToString("HHmmss"); // 143045

        return $"{enrollmentAbbreviation}.{partialLabel}.{schoolyear}.{type.ToUpper()}.{date}.{time}.xlsx";
    }
    
}