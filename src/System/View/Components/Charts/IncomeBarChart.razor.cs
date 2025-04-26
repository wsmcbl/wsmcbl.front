using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.Charts;

public partial class IncomeBarChart : ComponentBase
{
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    [Inject] private IJSRuntime js { get; set; } = null!;
    private RevenuesDto Data { get; set; } = new();
    private bool IsLoading { get; set; } = true;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await RenderChard();
        }
    }
    protected override async Task OnParametersSetAsync()
    {
        await LoadSummary();
        await RenderChard();
    }
    private async Task LoadSummary()
    {
        IsLoading = true;
        StateHasChanged();
        Data = await Controller.GetCurrentRevenues();
        Data.other = Data.totalIncomeThisMonth - Data.expectedIncomeReceived;
        IsLoading = false;
        StateHasChanged();
    }
    private async Task RenderChard()
    {
        try
        {
            await Task.Delay(50);
            await js.InvokeVoidAsync("renderIncomeBarChart", Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al renderizar gráfico: {ex.Message}");
        }
    }
}