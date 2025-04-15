using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Accounting.Charts;

public partial class MonthSumary : ComponentBase
{
    [Inject] DashboardCashierController Controller { get; set; } = null!;
    private SummaryThisMonthDto Sumary { get; set; } = new();
    [Parameter] public string Month { get; set; } = string.Empty;

    
    protected override async Task OnParametersSetAsync()
    {
        await LoadSummary();
    }
    private async Task LoadSummary()
    {
        Sumary = await Controller.GetRevenueMonth(Month);
    }
    private string FormatCurrency(decimal amount)
    {
        return amount.ToString("C2", new System.Globalization.CultureInfo("es-NI"));
    }
}