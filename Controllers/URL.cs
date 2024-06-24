namespace wsmcbl.front.Controllers;

public static class URL
{
    private static readonly string API_URL = "http://wsmcbl-api.somee.com/v1/";

    public static string SECRETARY = $"{API_URL}secretary/";




    public static string ACCOUNTING = $"{API_URL}accounting/";
    public static string TRANSACTION = $"{ACCOUNTING}transactions/invoices/";
    public static string APPLYARREARS = $"{API_URL}accounting/arrears/";
}