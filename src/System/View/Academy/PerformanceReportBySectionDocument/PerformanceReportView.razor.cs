using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Academy.PerformanceReportBySectionDocument;

public partial class PerformanceReportView : ComponentBase
{
    [Inject] private GeneratePerformanceReportBySection Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public string? EnrollmentName { get; set; }
    private int PartialId { get; set; } = 0;

    private async Task DownloadReport()
    {
        if (TeacherId != null && !string.IsNullOrEmpty(EnrollmentName))
        {
            await Controller.GetEnrollmentGradeByTeacherXlsx(TeacherId, PartialId, EnrollmentName);
        }
    }
}