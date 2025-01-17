using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.Reports;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.Transactions;

public partial class TransactionListView : BaseView
{
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private TransactionReportByDateController controller { get; set; } = null!;
    [Inject] private CollectTariffController CollectTariffController { get; set; } = null!;
    
    private byte[]? InvoicePdf { get; set; }
    private List<TransactionFullDto>? transactionList { get; set; }
    private List<TransactionTypeDto>? transactionTypeList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadData();
    }
    
    private async Task LoadData()
    {
        transactionTypeList = await controller!.GetTypeTransactions();
        transactionList = await controller!.GetTransactions();
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
            
        await Notificator.ShowSuccess("Transacción anulada correctamente.");
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
}