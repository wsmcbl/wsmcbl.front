using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.Reports;

public partial class RevenueReportView : BaseView
{
    [Inject] private Notificator notificator { get; set; } = null!;
    [Inject] private TransactionReportByDateController controller { get; set; } = null!;

    private bool hasData { get; set; }
    private TransactionsRevenuesDto? report { get; set; }
    private List<TransactionTypeDto>? transactionTypeList { get; set; }
    
    private DateOnly startDate { get; set; }
    private DateOnly endDate { get; set; }
    private string MaxDate { get; set; } = DateTime.Today.ToString("dd/MM/yyyy");

    protected override async Task OnParametersSetAsync()
    {
        SetDefaultDate();
        report = new TransactionsRevenuesDto();
        await LoadTypeTransactions();
    }

    private void SetDefaultDate()
    {
        endDate = DateOnly.FromDateTime(DateTime.Today);
        startDate = endDate;
    }
    
    private async Task<bool> ValidateDate()
    {
        if (startDate.Year < 2000 || endDate.Year < 2000)
        {
            await notificator.ShowInformation("Alguna de las fechas ingresadas no es válida. Por favor, verifíquelas.");
            return false;
        }

        if (startDate <= endDate)
        {
            return true;
        }
        
        await notificator.ShowInformation("La fecha de inicio no puede ser posterior a la fecha de finalización.");
        return false;
    }

    private async Task LoadTypeTransactions()
    {
        transactionTypeList = await controller.GetTypeTransactions();
    }

    private async Task GetReport()
    {
        if (!await ValidateDate())
        {
            return;
        }
        
        ClearData(false);
        
        report = await controller.GetReport(startDate, endDate);
        hasData = report.transactionList.Count > 0;
        StateHasChanged();
    }

    private void ClearData(bool withDate = true)
    {
        report!.transactionList = [];
        hasData = false;

        if (withDate)
        {
            SetDefaultDate();
        }
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