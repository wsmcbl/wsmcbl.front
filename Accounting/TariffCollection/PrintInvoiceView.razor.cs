using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controllers;
using wsmcbl.front.dto.input;

namespace wsmcbl.front.Accounting.TariffCollection;

public class PrintInvoice : ComponentBase
{
    [Inject] protected TariffCollectionController controller { get; set; } = null!;
    [Inject] protected AlertService alertService { get; set; } = null!;
    
    protected InvoiceDto? invoice { get; set; }
    protected string? errorMessage { get; set; }
    
    [Parameter] public string? transactionId { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new Exception("Transaction ID is not valid");
            }
            
            invoice =  await controller.GetInvoice(transactionId);
        }
        catch(Exception e)
        {
            errorMessage = e.Message;
        }     
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && errorMessage != null)
        {
            await alertService.AlertError("Obtuvimos unos problemas",errorMessage);
        }
    }
}