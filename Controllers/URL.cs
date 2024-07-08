namespace wsmcbl.front.Controllers;

public static class URL
{
    private static readonly string api = "http://wsmcbl-api.somee.com/v1";
    
    public static readonly string secretary = $"{api}/secretary/";
    public static readonly string accounting = $"{api}/accounting/";
    
    public static string TRANSACTION = $"{accounting}transactions/invoices/";
    public static string APPLYARREARS = $"{api}/accounting/arrears/";
}