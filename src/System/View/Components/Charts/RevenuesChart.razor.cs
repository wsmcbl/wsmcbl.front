using System.Globalization;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.Charts;

public partial class RevenuesChart : ComponentBase
{
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    private RevenuesDto CurrentRevenues { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }
    private async Task LoadData()
    {
        CurrentRevenues = await Controller.GetCurrentRevenues();
    }
    private string FormatNicaraguanCurrency(decimal value)
    {
        var niCulture = CultureInfo.CreateSpecificCulture("es-NI");
        niCulture.NumberFormat.CurrencySymbol = "C$";
        return value.ToString("C", niCulture);
    }
}