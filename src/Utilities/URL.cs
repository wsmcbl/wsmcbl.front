namespace wsmcbl.src.Service;

public static class URL
{
    private static readonly string api = "http://wsmcbl-api.somee.com/v1";
    
    public static readonly string secretary = $"{api}/secretary/";

    public static string ListSchoolYears = $"{secretary}configurations/schoolyears?q=all";
    public static string NewSchoolYear = $"{secretary}configurations/schoolyears?q=new";
    public static string PostSchoolYear = $"{secretary}configurations/schoolyears";
    public static string NewSchoolYearTariff = $"{secretary}configurations/schoolyears/tariffs";
    public static string NewSubject = $"{secretary}configurations/schoolyears/subjects";
    
    public static string GetTeacherBasic = $"{secretary}teachers/";
    public static string ConfigurateEnrollment = $"{secretary}degrees/";
    public static string DegreesEnrollments = $"{ConfigurateEnrollment}enrollments";
}