using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.Statistics;

public partial class StatisticsView : BaseView
{
    [Inject] GenerateEvaluationStatsBySectionController Controller { get; set; } = null!;
    [Parameter] public string TeacherId { get; set; } = string.Empty;
    [Parameter] public int PartialId { get; set; } = 1;
    private EvaluationSumaryDto? EvaluationSumary { get; set; }

    
    
    protected override async Task OnParametersSetAsync()
    {
        EvaluationSumary = await Controller.GetStatsSummary(TeacherId, PartialId);
    }
}