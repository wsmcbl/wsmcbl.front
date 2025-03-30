using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.Charts;

public partial class GradeChart : ComponentBase
{
    [Inject] IJSRuntime js { get; set; } = null!;
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    private DistributionDto CurrentDistribution { get; set; } = new();

    private List<string> level = [];
    private List<int> total = [];
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    { 
        CurrentDistribution = await Controller.GetCurrentDistribution();
        GetData();
        await js.InvokeVoidAsync("createDegreeChart", level, total);
    }

    private void GetData()
    {
        foreach (var item in CurrentDistribution.degreeList.OrderBy(t => t.position))
        {
            level.Add(item.label.Split(' ')[0]);
            total.Add(item.total);
        }
    }
}