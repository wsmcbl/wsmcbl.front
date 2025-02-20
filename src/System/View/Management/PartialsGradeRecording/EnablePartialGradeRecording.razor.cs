using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Management.PartialsGradeRecording;

public partial class EnablePartialGradeRecording : BaseView
{
    [Inject] public EnablePartialGradeRecordingController Controller { get; set; } = default!;
    [Inject] public Navigator Navigator { get; set; } = default!;
    private List<PartialListDto> Partials { get; set; } = new();
    
    private string SemesterForActive { get; set; } = string.Empty;
    private string DateRange { get; set; } = string.Empty;
    private string PartialNameForActive { get; set; } = string.Empty;
    private int PartialIdForActive { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Partials = await Controller.GetPartials();
    }
    
    protected override bool IsLoading()
    {
        return Partials == null;
    }
    
    private async Task EnablePartials(string semester, int partial, string dateRange, string partialNameForActive)
    {
        SemesterForActive = semester;
        PartialIdForActive = partial;
        DateRange = dateRange;
        PartialNameForActive = partialNameForActive;
        await Navigator.ShowModal("ActivePartialsModal");
    }
}