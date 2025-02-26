using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.Reports.Revenue;

public partial class RevenueReportView : BaseView
{
    [Inject] private Notificator notificator { get; set; } = null!;
    [Inject] private TransactionReportByDateController controller { get; set; } = null!;
    
    private TransactionsRevenuesDto? report { get; set; }
    private List<TransactionTypeDto>? transactionTypeList { get; set; }
    private DateOnly startDate { get; set; }
    private DateOnly endDate { get; set; }
    private string MaxDate { get; set; } = DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd");
    
    //var for paginator
    private PagedRequest Request { get; set; } = new();
    private bool hasData {get; set;}
    
    protected override async Task OnParametersSetAsync()
    {
        SetDefaultDate();
        await LoadTypeTransactions();
        report = new TransactionsRevenuesDto();
        hasData = report.data.Count > 0;
    }
    private void SetDefaultDate()
    {
        endDate = DateOnly.FromDateTime(DateTime.UtcNow.AddHours(-6));
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
        
        report = await controller.GetReport(startDate, endDate, Request);
        hasData = report.data.Count > 0;
        StateHasChanged();
    }
    private void ClearData(bool withDate = true)
    {
        report!.data = new ();
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
    
    //Method for paginator
    private async Task SortByColumn(string columnName)
    {
        if (Request.sortBy == columnName)
        {
            Request.isAscending = !Request.isAscending;
        }
        else
        {
            Request.sortBy = columnName;
            Request.isAscending = true;
        }

        Request.sortBy = columnName;
        await GetReport();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await GetReport();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= report!.totalPages)
        {
            Request.CurrentPage = pageNumber;
            await GetReport();
        }
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await GetReport();
            if (report != null) hasData = report.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await GetReport();
    }
}