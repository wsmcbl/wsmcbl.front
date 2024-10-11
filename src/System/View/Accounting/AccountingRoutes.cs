namespace wsmcbl.src.View.Accounting;

public static class AccountingRoutes
{
    
    /*Mejorar los nombre los recursos*/
    
    public const string ProfileList = "/accounting/profiles";
    public const string ArrearsList = "/accouting/arreas";
    public const string TariffCollection = "/accouting/transactions/invoices/{transactionId}";
    public const string PrintInvoice = "/accouting/tariffcollection/{StudentID}";
    
    
    
    /*
     *
     * @page "@Accounting.ArrearsList"
       
       <h1>Customers</h1>
       
       <a href="@Accounting.PrintInvoice.Replace("{StudentID}", "Hola")">Create New Customer</a>
       
       @* Rest of the page implementation *@
     */
}