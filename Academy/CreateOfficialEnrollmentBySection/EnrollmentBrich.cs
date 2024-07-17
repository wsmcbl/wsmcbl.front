using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace wsmcbl.front.Academy.CreateOfficialEnrollmentBySection;

public class EnrollmentBrich_razor : ComponentBase
{
    protected int SecctionsNumber;
    protected bool tabsCreated;
    [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;
    
    protected void CreateTabs(int numberOfTabs)
    {
        if (numberOfTabs > 0 && numberOfTabs <= 7)
        {
            tabsCreated = true;
            OpenNewTab();
        }
        else
        {
            tabsCreated = false;
        }
    }
    
    protected async Task OpenNewTab()
    {
        await JSRuntime.InvokeVoidAsync("window.open", $"/academy/enrollments/configuration/{SecctionsNumber}", "_blank");
    }
    
}