using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Management.PartialsGradeRecording;

namespace wsmcbl.src.View.Academy.SemestralReport;

public partial class SemestralReportView : BaseView
{
    [Inject] public EnablePartialGradeRecordingController Controller { get; set; } = null!;
    [Inject] private GeneratePerformanceReportBySection generatePerformanceReportBySectionController { get; set; } = null!;

    [Parameter] public bool IsGuide  { get; set; }
    [Parameter] public string TeacherId { get; set; } = string.Empty;
    private List<PartialDto> Partials { get; set; } = new();
    private HashSet<int> SelectedPartialIds { get; set; } = new HashSet<int>();
    
    protected override async Task OnInitializedAsync()
    {
        var list = await Controller.GetPartials();
        Partials = list.OrderBy(t => t.partialId).ToList();
    }
    
    private void OnCheckboxChanged(ChangeEventArgs e, int partialId)
    {
        if (e.Value is bool isChecked)
        {
            if (isChecked)
            {
                SelectedPartialIds.Add(partialId);
            }
            else
            {
                SelectedPartialIds.Remove(partialId);
            }
        }
    }
    
    private async Task GenerarReporte()
    {
        List<int> idsParaReporte = SelectedPartialIds.ToList();
        
        var firstSelectedPartial = Partials?.FirstOrDefault(p => p.partialId == idsParaReporte.FirstOrDefault());
        string? semesterName = firstSelectedPartial?.semester;

        await generatePerformanceReportBySectionController.GetSemestralReportByPartialsIdXlsx(TeacherId, idsParaReporte, semesterName);
    }

    protected override bool IsLoading()
    {
        return !IsGuide;
    }
}