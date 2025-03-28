using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;

namespace wsmcbl.src.View.Components.Charts;

public partial class SumaryStudentChart : ComponentBase
{ 
    [Inject] DirectorDashboardController Controller { get; set; } = null!;
    private DistributionDto CurrentDistribution { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        CurrentDistribution = await Controller.GetCurrentDistribution();
    }
}