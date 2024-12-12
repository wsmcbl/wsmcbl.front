using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Accounting.Reports;

public partial class RevenueReport : ComponentBase
{
    [Inject] TransactionReportByDateController? Controller { get; set; }
    private TransactionsRevenuesDto? Transactions { get; set; }
    private List<TypeTransactionsDto>? TypeTransactions { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        Transactions = new TransactionsRevenuesDto();
         await LoadTypeTransactions();
    }
    
    private async Task LoadTypeTransactions()
    {
        TypeTransactions = await Controller!.GetTypeTransactions();
    }
    
    private async Task GetTransactionsRevenuesAsync(int type)
    {
        Transactions = await Controller!.GetTransactionsRevenues(type);
        StateHasChanged();
    }
    
    private bool IsLoad()
    {
        return false;
    }
}
