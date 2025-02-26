using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports;
using wsmcbl.src.View.Accounting.Reports.Revenue;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.Transactions;

public partial class TransactionListView : BaseView
{
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private TransactionReportByDateController controller { get; set; } = null!;
    [Inject] private CollectTariffController CollectTariffController { get; set; } = null!;
    
    private byte[]? InvoicePdf { get; set; } 
    private List<TransactionTypeDto>? transactionTypeList { get; set; }
    
    //var for paginator
    private PagedRequest Request { get; set; } = new();
    private Paginator<TransactionFullDto>? transactionList { get; set; }
    private bool hasData {get; set;}

    protected override async Task OnParametersSetAsync()
    {
        transactionTypeList = await controller!.GetTypeTransactions();
        await LoadData();
    }
    private async Task LoadData()
    {
        transactionList = await controller!.GetTransactions(Request);
        hasData = transactionList.data.Count > 0;

    }
    private async Task GetInvoicePdf(string transactionId)
    {
        InvoicePdf = await CollectTariffController!.GetInvoice(transactionId);
        await Navigator.ShowPdfModal();
    }
    private async Task CancelTransactions(string transactionId)
    {
        var result = await Notificator
            .ShowAlertQuestion("Advertencia", $"¿Está seguro de anular la transacción {transactionId}?",
                ("Sí", "No"));
        if (!result)
        {
            return;
        }
        
        var response = await CollectTariffController!.CancelTransaction(transactionId);
        if (!response)
        {
            await Notificator.ShowError("Hubo un fallo al anular la transacción.");
            return;
        }
            
        await Notificator.ShowSuccess("Se ha anulado la transacción correctamente.");
        await LoadData();
        StateHasChanged();
    }
    private string getTransactionDescription(int type)
    {
        return transactionTypeList!
            .FirstOrDefault(t => t.typeId == type)?.description ?? "Descripción no disponible";
    }
    protected override bool IsLoading()
    {
        return transactionList == null || transactionTypeList == null;
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
        await LoadData();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await LoadData();
        }
        else
        {
            Console.WriteLine("Error: No se pudo convertir el valor seleccionado a entero.");
        }
    }
    private async Task ShowPage(int pageNumber)
    {
        if (pageNumber >= 1 && pageNumber <= transactionList!.totalPages)
        {
            Request.CurrentPage = pageNumber;
            await LoadData();
        }
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await LoadData();
            if (transactionList != null) hasData = transactionList.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await LoadData();
    }
    
}