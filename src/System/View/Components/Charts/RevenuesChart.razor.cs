using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.Charts;

public partial class RevenuesChart : ComponentBase
{
    [Inject] IJSRuntime js { get; set; } = null!;
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    private RevenuesDto CurrentRevenues { get; set; } = new() { expectedIncomeThisMonth = 0, expectedIncomeReceived = 0, totalIncomeThisMonth = 0 };
    
    private decimal amountRecivedExpentPorcent => CurrentRevenues.totalIncomeThisMonth != 0 
    ? (CurrentRevenues.expectedIncomeReceived / CurrentRevenues.totalIncomeThisMonth)
    : 0;
    
    private decimal otherAmountPorcent => CurrentRevenues.totalIncomeThisMonth != 0 
    ? ((CurrentRevenues.totalIncomeThisMonth - CurrentRevenues.expectedIncomeReceived) / CurrentRevenues.totalIncomeThisMonth)
    : 0;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        CurrentRevenues = await Controller.GetCurrentRevenues();
        StateHasChanged();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    { 
        await RenderCharts();
    }
    
    private async Task RenderCharts()
    {
        var chartData = new
        {
            barLabels = new[] { "Ingresos" },
            expected = (double)CurrentRevenues.expectedIncomeThisMonth,
            receivedExpected = (double)CurrentRevenues.expectedIncomeReceived,
            
            pieData = new
            {
                receivedExpected = (double)CurrentRevenues.expectedIncomeReceived,
                receivedOther = (double)(CurrentRevenues.totalIncomeThisMonth-CurrentRevenues.expectedIncomeReceived),
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