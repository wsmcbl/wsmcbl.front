using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Management.PartialsGradeRecording;

public partial class EnablePartialGradeRecordingView : BaseView
{
    [Inject] public EnablePartialGradeRecordingController Controller { get; set; } = null!;
    [Inject] public Navigator Navigator { get; set; } = null!;
    [Inject] public Notificator Notificator { get; set; } = null!;
    private List<PartialDto> Partials { get; set; } = new();
    
    private string SemesterForActive { get; set; } = string.Empty;
    private string DateRange { get; set; } = string.Empty;
    private string PartialNameForActive { get; set; } = string.Empty;
    private int PartialIdForActive { get; set; }
    private bool IsActive { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Partials = await Controller.GetPartials();
    }
    
    protected override bool IsLoading()
    {
        return Partials == null;
    }
    
    private async Task EnableGradeRecording(string semester, int partial, string dateRange, string partialNameForActive)
    {
        SemesterForActive = semester;
        PartialIdForActive = partial;
        DateRange = dateRange;
        PartialNameForActive = partialNameForActive;
        await Navigator.ShowModal("ActivePartialsGradeModal");
    }

    private async Task EnablePartials(string itemSemester, int itemPartialId, string itemPeriod, string itemLabel, bool itemisActive)
    {
        SemesterForActive = itemSemester;
        PartialIdForActive = itemPartialId;
        DateRange = itemPeriod;
        PartialNameForActive = itemLabel;
        IsActive = itemisActive;
        await Navigator.ShowModal("ActivePartialsModal");
    }
    
    private async Task DisableGradeRecords(int itemPartialId)
    {
        var respo = await Notificator.ShowAlertQuestion("Advertencia","Estas seguro de desactivar el registro de calificaciones", ("Seguro","Cancelar"));
        if (respo)
        {
            var endTime = DateTime.Now.ToString();
            var response = await Controller.ActiveGradesRecording(itemPartialId, false, endTime);
            if (response)
            {
                await Notificator.ShowSuccess("Hemos deshabilitado con exito el registro de calificaciones");
                return;
            }
            await Notificator.ShowError("No hemos podido deshabilitar el registro de calificaciones");
        }
    }
}