using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.Charts;

public partial class EducationLevesChart : ComponentBase
{
    [Inject] IJSRuntime js { get; set; } = null!;
    [Inject] DirectorDashboardController Controller { get; set; } = null!;
    private DistributionDto CurrentDistribution { get; set; } = new();

    private List<string> level = [];
    private List<int> total = [];

    protected override async Task OnAfterRenderAsync(bool firstRender)
    { 
        CurrentDistribution = await Controller.GetCurrentDistribution();
        GetData();
        await js.InvokeVoidAsync("createDynamicChart", level, total);
    }

    private void GetData()
    {
        foreach (var item in CurrentDistribution.levelList)
        {
            level.Add(item.educationalLevel.ToLevel());
            total.Add(item.total);
        }
    }
}