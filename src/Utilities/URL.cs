namespace wsmcbl.src.Service;

public static class URL
{
    private static readonly string api = "http://wsmcbl-api.somee.com/v1";
    private static readonly string local = "http://localhost:4000/v1";
    
    public static readonly string secretary = $"{api}/secretary/";
    public static string ConfigurateEnrollment = $"{secretary}degrees/";
    public static string DegreesEnrollments = $"{ConfigurateEnrollment}enrollments";
}