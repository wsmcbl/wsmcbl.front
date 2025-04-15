using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Accounting.Charts;

public partial class MonthSumary : ComponentBase
{
    [Inject] DashboardCashierController Controller { get; set; } = null!;
    private SummaryThisMonthDto Sumary { get; set; } = new();
    private List<(string Display, string Value)> months = null!;
    private string selectedMonthValue = "N/A";
    private string selectedMonthDisplay = "N/A";
    
    protected override async Task OnParametersSetAsync()
    {
        months = GenerateMonthList(); 
        selectedMonthValue = months.First().Value;
        selectedMonthDisplay = months.First().Display;
        await LoadSummary();
    }
    private async Task LoadSummary()
    {
        Sumary = await Controller.GetRevenueMonth(selectedMonthValue);
    }
    private static List<(string Display, string Value)> GenerateMonthList()
    {
        var months = new List<(string Display, string Value)>();
        var currentDate = DateTime.Now;

        for (int i = 0; i <= 17; i++)
        {
            var date = currentDate.AddMonths(-i);
            string display = date.ToString("MMMM/yyyy", new System.Globalization.CultureInfo("es-ES"));
            string value = date.ToString("MM-yyyy");
            months.Add((display, value));
        }

        return months;
    }
    private async Task OnMonthSelected((string Display, string Value) month)
    {
        selectedMonthValue = month.Value;
        selectedMonthDisplay = month.Display;
        await LoadSummary();
    }
    private string FormatCurrency(decimal amount)
    {
        return amount.ToString("C2", new System.Globalization.CultureInfo("es-NI"));
    }
}