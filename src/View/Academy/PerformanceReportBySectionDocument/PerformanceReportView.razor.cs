using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Academy.PerformanceReportBySectionDocument;

public partial class PerformanceReportView : ComponentBase
{
    [Inject] private GeneratePerformanceReportBySection Controller { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;  
    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public string? EnrollmentName { get; set; }
    private int PartialId { get; set; } = 1;
    private List<PartialEntity> Partial { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        Partial = await AddingStudentGradesController.GetPartialList();
        PartialId = Partial.FirstOrDefault(t => t.isActive)?.partialId ?? 1;     
    }

    private async Task DownloadReport()
    {
        if (TeacherId != null && !string.IsNullOrEmpty(EnrollmentName))
        {
            var partialLabel = Partial.FirstOrDefault(t => t.partialId == PartialId)?.label ?? "N/A";
            await Controller.GetEnrollmentGradeByTeacherXlsx(TeacherId, PartialId, partialLabel, EnrollmentName);
        }
    }
}