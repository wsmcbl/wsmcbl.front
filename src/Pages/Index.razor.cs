using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace wsmcbl.src.Pages;

public class Index_razor : ComponentBase
{
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    
    private List<string> months = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio" };
    private List<int> sales = new List<int> { 500, 600, 700, 800, 900, 1500 };

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("createDynamicChart", months, sales);
        }
    }
    
}