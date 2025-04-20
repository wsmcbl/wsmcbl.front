using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Accounting.Charts;

public partial class MonthDistribution : ComponentBase
{
    [Inject] DashboardCashierController Controller { get; set; } = null!;
    [Parameter] public string Month { get; set; } = string.Empty;
    [Inject] IJSRuntime JS { get; set; } = null!;
    private SummaryThisMonthDto Sumary { get; set; } = new();
    private DistributionLevelsDto Data { get; set; } = new();
    private bool IsLoading { get; set; }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await RenderChard();
    }
    protected override async Task OnParametersSetAsync()
    {
        await LoadSummary();
    }
    private async Task LoadSummary()
    {
        IsLoading = true;
        Data = await Controller.GetDistibution(Month);
        Sumary = await Controller.GetRevenueMonth(Month);
        IsLoading = false;
        StateHasChanged();
    }
    private async Task RenderChard()
    {
        try
        {
            await Task.Delay(50);
            await JS.InvokeVoidAsync("levelsDonutChart", Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al renderizar gráfico: {ex.Message}");
        }
    }
    private string FormatCurrency(decimal amount)
    {
        return amount.ToString("C2", new System.Globalization.CultureInfo("es-NI"));
    }
    private string CalculatePercentage(decimal partial, decimal total)
    {
        if (total == 0) return "0";
        return Math.Round((partial / total) * 100, 1).ToString("0.0");
    }
    
    private string CalculatePercentageForProgressBar(decimal partial, decimal total)
    {
        if (total == 0) return "0";
        return Math.Round((partial / total) * 100, 1).ToString("0.0", CultureInfo.InvariantCulture);
    }

}