using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.Reports;

public partial class RevenueReportView : BaseView
{
    [Inject] private TransactionReportByDateController controller { get; set; } = null!;

    private bool hasData { get; set; }
    private TransactionsRevenuesDto? report { get; set; }
    private List<TransactionTypeDto>? transactionTypeList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        report = new TransactionsRevenuesDto();
        await LoadTypeTransactions();
    }

    private async Task LoadTypeTransactions()
    {
        transactionTypeList = await controller.GetTypeTransactions();
    }

    private async Task GetTransactionsRevenuesAsync(int type)
    {
        report = await controller.GetReport(type);
        hasData = true;
        StateHasChanged();
    }

    private void ClearData()
    {
        hasData = false;
    }

    protected override bool IsLoading()
    {
        return hasData;
    }

    private string getTransactionDescription(int type)
    {
        return transactionTypeList!
            .FirstOrDefault(t => t.typeId == type)?.description ?? "Descripción no disponible";
    }
}