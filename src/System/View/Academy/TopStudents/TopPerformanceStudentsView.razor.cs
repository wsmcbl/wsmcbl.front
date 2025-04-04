using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.TopStudents;

public partial class TopPerformanceStudentsView : BaseView
{
    [Inject] private GeneratePerformanceReportBySection Controller { get; set; } = null!;
    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public int PartialId { get; set; }
    [Parameter] public bool Isguide { get; set; }
    private List<TopStudentsDto>? Students { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        if (TeacherId != null && PartialId > 0 && Isguide)
        {
            Students = await Controller.GetTopStudents(TeacherId, PartialId);
        }
    }
    protected override bool IsLoading()
    {
        return Students == null;
    }
}