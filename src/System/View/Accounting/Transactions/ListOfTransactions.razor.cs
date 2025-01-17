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
    [Inject] private Notificator Notificator { get; set; } = null!;
    
    private List<TransactionsFullDto> Transactions = [];
    private List<TransactionTypeDto> TypeTransactions { get; set; } = null!;
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
    
    private async Task CancelTransactions(string transactionId)
    {
        var result = await Notificator
            .ShowAlertQuestion("Advertencia", $"¿Está seguro de anular la transacción {transactionId}?",
                ("Sí", "No"));
        
        if (result)
        {
            var response = await CollectTariffController!.CancelTransaction(transactionId);
            if (!response)
            {
                await Notificator.ShowError("Hubo un fallo al anular la transacción.");
                return;
            }
            
            await Notificator.ShowSuccess("Transacción anulada correctamente.");
            await LoadData();
            StateHasChanged();
        }
    }
    
    
}