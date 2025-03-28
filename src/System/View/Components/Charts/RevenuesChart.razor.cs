using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.Charts;

public partial class RevenuesChart : ComponentBase
{
    [Inject] IJSRuntime js { get; set; } = null!;
    [Inject] DirectorDashboardController Controller { get; set; } = null!;
    
    private RevenuesDto CurrentRevenues { get; set; } = new() 
    {
        expectedIncomeThisMonth = 0,
        expectedIncomeReceived = 0,
        totalIncomeThisMonth = 0
    };
    
    private decimal CompletionPercentage => 
        CurrentRevenues.expectedIncomeThisMonth > 0 
            ? CurrentRevenues.expectedIncomeReceived / CurrentRevenues.expectedIncomeThisMonth 
            : 0;
            
    private decimal TotalVsExpectedPercentage =>
        CurrentRevenues.expectedIncomeThisMonth > 0
            ? CurrentRevenues.totalIncomeThisMonth / CurrentRevenues.expectedIncomeThisMonth
            : 0;

    private decimal AdditionalIncome =>
        CurrentRevenues.totalIncomeThisMonth - CurrentRevenues.expectedIncomeReceived;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
        await base.OnInitializedAsync();
    }

    private async Task LoadData()
    {
        CurrentRevenues = await Controller.GetCurrenRevenues();
        StateHasChanged();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    { 
        await RenderCharts();
    }
    
    private async Task RenderCharts()
    {
        var additionalIncome = AdditionalIncome;
        var pendingIncome = CurrentRevenues.expectedIncomeThisMonth - CurrentRevenues.expectedIncomeReceived;

        var chartData = new
        {
            barLabels = new[] { "Ingresos" },
            expected = (double)CurrentRevenues.expectedIncomeThisMonth,
            receivedExpected = (double)CurrentRevenues.expectedIncomeReceived,
            additional = (double)additionalIncome,
            pieData = new
            {
                receivedExpected = (double)CurrentRevenues.expectedIncomeReceived,
                pending = (double)pendingIncome,
                additional = (double)additionalIncome
            }
        };

        await js.InvokeVoidAsync("RevenueChart", chartData);
    }

    private string FormatNicaraguanCurrency(decimal value)
    {
        var niCulture = CultureInfo.CreateSpecificCulture("es-NI");
        niCulture.NumberFormat.CurrencySymbol = "C$";
        return value.ToString("C", niCulture);
    }

    private string FormatPercentage(decimal value)
    {
        return value.ToString("P1", CultureInfo.InvariantCulture);
    }
}