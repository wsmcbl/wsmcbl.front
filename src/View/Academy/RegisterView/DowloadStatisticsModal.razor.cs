using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Academy;

namespace wsmcbl.src.View.Academy.RegisterView;

public partial class DowloadStatisticsModal : ComponentBase
{
    [Parameter] public string TeacherId { get; set; } = "N/A";
    [Parameter] public string EnrollmentLabel { get; set; } = "N/A";
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;  
    [Inject] private ViewEnrollmentGuideController GuideController { get; set; } = null!;
    
    private int CurrentPartialId { get; set; } = 1;
    private int CurrentPartialLabel { get; set; } = 1;
    private List<PartialEntity> Partial { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Partial = await AddingStudentGradesController.GetPartialList();
        CurrentPartialId = Partial.FirstOrDefault(t => t.isActive)?.partialId ?? 1;     
    }

    private async Task DownloadPartialReport()
    {
        var partialLabel = Partial.FirstOrDefault(t => t.partialId == CurrentPartialId)?.label ?? "N/A";
        await GuideController.GetStatisticsDocument(TeacherId, CurrentPartialId, partialLabel, EnrollmentLabel);
    }
}