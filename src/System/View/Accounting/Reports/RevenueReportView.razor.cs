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
    
    private DateOnly startDate { get; set; }
    private DateOnly endDate { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        report = new TransactionsRevenuesDto();
        await LoadTypeTransactions();
    }

    private async Task LoadTypeTransactions()
    {
        transactionTypeList = await controller.GetTypeTransactions();
    }

    private async Task GetReport()
    {
        ClearData();
        
        report = await controller.GetReport(startDate, endDate);
        hasData = report.transactionList.Count > 0;
        StateHasChanged();
    }
    
    private async Task GetReportToday()
    {
        endDate = DateOnly.FromDateTime(DateTime.Today);
        startDate = endDate;   
        
        await GetReport();
    }

    private void ClearData()
    {
        report!.transactionList = [];
        hasData = false;
    }

    protected override bool IsLoading()
    {
        return report == null || transactionTypeList == null;
    }

    private string getTransactionDescription(int type)
    {
        return transactionTypeList!
            .FirstOrDefault(t => t.typeId == type)?.description ?? "Descripción no disponible";
    }
}