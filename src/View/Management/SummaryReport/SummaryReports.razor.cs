using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Management.SummaryReport;

public partial class SummaryReports : ComponentBase
{    
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;  
    [Inject] private ViewPrincipalDashboardController ViewPrincipalDashboardController { get; set; } = null!;  
    [Inject] private Navigator Navigator { get; set; } = null!;  
    
    private int CurrentPartialId { get; set; } = 1;
    private List<PartialEntity> Partial { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        Partial = await AddingStudentGradesController.GetPartialList();
        CurrentPartialId = Partial.FirstOrDefault(t => t.isActive)?.partialId ?? 1;     
    }
    
    private async Task DownloadReport()
    {
        var partialName = Partial.FirstOrDefault(p => p.partialId == CurrentPartialId)?.label ?? "Parcial no encontrado";
        await ViewPrincipalDashboardController.GetSummaryReport(CurrentPartialId, partialName);
        await Navigator.HideModal("DownloadSummaryModal");
    }

    private async Task DownloadReportFailed()
    {
        var partialName = Partial.FirstOrDefault(p => p.partialId == CurrentPartialId)?.label ?? "Parcial no encontrado";
        await ViewPrincipalDashboardController.GetSummaryReportFailed(CurrentPartialId, partialName);
        await Navigator.HideModal("DownloadSummaryFailedModal");
    }
}