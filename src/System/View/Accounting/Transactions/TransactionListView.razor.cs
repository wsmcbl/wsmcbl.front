using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;
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
    private PagedRequest Request { get; set; } = new PagedRequest
    {
        sortBy = "dateTime",
        isAscending = false
    };

    
    private Paginator<TransactionFullDto>? transactionList { get; set; }
    private bool hasData {get; set;}

    protected override async Task OnParametersSetAsync()
    {
        transactionTypeList = await controller!.GetTypeTransactions();
        UpdateRequest();
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
    private Task UpdateUrl()
    {
        var uri = $"/accounting/reports/transactions{Request.ToString()}";
        Navigator.UpdateUrl(uri);
        return Task.CompletedTask;
    }
    private void  UpdateRequest()
    {
        var uri = new Uri(Navigator.GetUrl());
        var queryParams = QueryHelpers.ParseQuery(uri.Query);

        if (queryParams.TryGetValue("search", out var search))
        {
            Request.SearchText = search;
        }

        if (queryParams.TryGetValue("sortBy", out var sortBy))
        {
            Request.sortBy = sortBy;
        }

        if (queryParams.TryGetValue("isAscending", out var isAscending))
        {
            Request.isAscending = bool.Parse(isAscending!);
        }

        if (queryParams.TryGetValue("page", out var page))
        {
            Request.CurrentPage = int.Parse(page!);
        }

        if (queryParams.TryGetValue("pageSize", out var pageSize))
        {
            Request.pageSize = int.Parse(pageSize!);
        }

        if (queryParams.TryGetValue("quantity", out var quantity))
        {
            Request.Quantity = int.Parse(quantity!);
        }
    }
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
        await UpdateUrl();
        await LoadData();
    }
    private async Task ShowPageSize(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int selectedValue))
        {
            Request.pageSize = selectedValue;
            Request.CurrentPage = 1;
            await UpdateUrl();
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
            await UpdateUrl();
            await LoadData();
        }
    }
    private async Task Searching(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            hasData = false;
            await UpdateUrl();
            await LoadData();
            if (transactionList != null) hasData = transactionList.data.Count > 0;
        }
    }
    private async Task ClearSearch()
    {
        Request.SearchText = string.Empty;
        await UpdateUrl();
        await LoadData();
    }
    private async Task GoToPreviousPage() => await ShowPage(Request.CurrentPage - 1);
    private async Task GoToNextPage() => await ShowPage(Request.CurrentPage + 1);
    
}