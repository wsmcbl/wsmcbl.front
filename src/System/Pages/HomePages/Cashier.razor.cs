using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.Pages.HomePages;

public partial class Cashier : ComponentBase
{
    [Inject] GenerateDebtorReportController Controller { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;
    private byte[]? Pdf { get; set; } = [];
    
    private List<(string Display, string Value)> months = null!;
    private string selectedMonthValue = "N/A";
    private string selectedMonthDisplay = "N/A";
    
    
    protected override Task OnParametersSetAsync()
    {
        months = GenerateMonthList(); 
        selectedMonthValue = months.First().Value;
        selectedMonthDisplay = months.First().Display;
        return Task.CompletedTask;
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
    private void OnMonthSelected((string Display, string Value) month)
    {
        selectedMonthValue = month.Value;
        selectedMonthDisplay = month.Display;
        StateHasChanged();
    }

    private async Task GetPdfContent()
    { 
        Pdf = await Controller.GetDebtorReport();
        if (Pdf.Length == 0)
        {
            await Notificator.ShowError("No se pudo generar el documento.");
            return;
        }
        
        await Navigator.ShowPdfModal();
    }
}