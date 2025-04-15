using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Accounting.Charts;

public partial class MonthDetails : ComponentBase
{
    [Inject] DashboardCashierController Controller { get; set; } = null!;
    [Inject] IJSRuntime JS { get; set; } = null!;
    private DetailsThisMonthDto Details { get; set; } = new();
    [Parameter] public string Month { get; set; } = string.Empty;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    { 
        await RenderChard();
    }
    protected override async Task OnParametersSetAsync()
    {
        Details = await Controller.GetDetails(Month);
    }
    private string FormatCurrency(decimal amount)
    {
        return amount.ToString("C2", new System.Globalization.CultureInfo("es-NI"));
    }
    private async Task RenderChard()
    {
        await JS.InvokeVoidAsync("RevenueChart", Details);
    }

}