namespace wsmcbl.front.Service;

public static class URL
{
    private static readonly string api = "http://wsmcbl-api.somee.com/v1";
    
    public static readonly string secretary = $"{api}/secretary/";
    public static readonly string accounting = $"{api}/accounting/";
    
    public static string TRANSACTION = $"{accounting}transactions/invoices/";
    public static string APPLYARREARS = $"{api}/accounting/arrears/";

    public static string ListSchoolYears = $"{secretary}configurations/schoolyears?q=all";
    public static string NewSchoolYear = $"{secretary}configurations/schoolyears?q=new";
    public static string PostSchoolYear = $"{secretary}configurations/schoolyears";
    public static string NewSchoolYearTariff = $"{secretary}configurations/schoolyears/tariffs";
    public static string NewSubject = $"{secretary}configurations/schoolyears/subjects";
    
    public static string EnrollStudentList = $"{secretary}enrollments/students";
    public static string GetInfoStudent = $"{secretary}enrollments/students/";
    public static string GetTeacherBasic = $"{secretary}teachers/";
    public static string ConfigurateEnrollment = $"{secretary}degrees/";
    public static string DegreesEnrollments = $"{ConfigurateEnrollment}enrollments";
}