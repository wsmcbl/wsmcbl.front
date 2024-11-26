using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;

namespace wsmcbl.src.View.Accounting.Reports;

public partial class RevenueReport : ComponentBase
{
    [Inject] TransactionReportByDateController Controller { get; set; }
    private TransactionsRevenuesDto Transactions { get; set; }
    private bool HasData { get; set; } = false;

    private async Task GetTransactionsRevenuesAsync(int type)
    {
        Transactions = await Controller.GetTransactionsRevenues(type);
        HasData = true;
        StateHasChanged();
    }
    
    private bool IsLoad()
    {
        return false;
    }
}
