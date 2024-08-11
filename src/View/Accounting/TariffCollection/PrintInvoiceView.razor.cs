using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class PrintInvoice : ComponentBase
{
    [Inject]
    protected CollectTariffController Controller { get; set; } = null!;
    
    [Parameter] 
    public string? TransactionId { get; set; }
    
    protected InvoiceDto? Invoice { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(TransactionId))
        {
            throw new InternalException("Transaction ID is not valid");
        }

        Invoice = await Controller.GetInvoice(TransactionId);
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            throw new InternalException("Obtuvimos unos problemas");
        }

        return Task.CompletedTask;
    }
}