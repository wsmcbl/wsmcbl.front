using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports;

namespace wsmcbl.src.View.Accounting.Transactions;

public partial class ListOfTransactions : ComponentBase
{
    [Inject] private TransactionReportByDateController? Controller { get; set; }
    [Inject] private CollectTariffController? CollectTariffController { get; set; }
    [Inject] private Navigator Navigator { get; set; } = null!;
    
    private List<TransactionsFullDto> Transactions = [];
    private List<TypeTransactionsDto> TypeTransactions { get; set; } = null!;
    private byte[]? InvoicePdf { get; set; }
    private bool IsLoad { get; set; } = true;

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
    }
    private async Task LoadData()
    {
        TypeTransactions = await Controller!.GetTypeTransactions();
        Transactions = await Controller!.GetTransactions();
        IsLoad = false;
    }
    
    private async Task GetInvoicePdf(string transactionId)
    {
        InvoicePdf = await CollectTariffController!.GetInvoice(transactionId);
        await Navigator.ShowPdfModal();
    }
    
    
}