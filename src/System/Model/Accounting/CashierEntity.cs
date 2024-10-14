using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Model.Accounting;

public class CashierEntity
{
    private string cashierId;
    private double arrear = 0.1;
    
    public CashierEntity(string cashierId)
    {
        this.cashierId = cashierId;
    }  
    
}