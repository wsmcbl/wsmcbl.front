using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Management.PartialsGradeRecording;

public partial class ActivePartialsComponent : ComponentBase
{
    [Inject] EnablePartialGradeRecordingController Controller { get; set; } = default!;
    [Inject] Notificator Notificator { get; set; } = default!;
    [Inject] Navigator Navigator { get; set; } = default!;
    [Parameter] public int PartialId { get; set; }
    [Parameter] public string PartialName { get; set; } = string.Empty;
    [Parameter] public string Semester { get; set; } = string.Empty;
    [Parameter] public string DateRange { get; set; } = string.Empty;
    [Parameter] public bool isActive {get; set;}
    [Parameter] public EventCallback IsActiveChanged { get; set; }

    private async Task ActivePartial(bool flag)
    {
        if (flag)
        {
            isActive = true;
            var response =  await Controller.ActivePartials(PartialId, isActive);
            if (response)
            {
                await Notificator.ShowSuccess("Exito","Hemos activado correctamente el parcial, ahora puedes habilitar el registro de calificaciones");
                await Navigator.HideModal("ActivePartialsModal");
                await IsActiveChanged.InvokeAsync();
                return;
            }
            await Notificator.ShowError("No hemos podido activar el parcial");
            await Navigator.HideModal("ActivePartialsModal");
        }
        else
        {
            isActive = false;
            var response =  await Controller.ActivePartials(PartialId, isActive);
            if (response)
            {
                await Notificator.ShowSuccess("Exito","Hemos desactivado correctamente el parcial");
                await Navigator.HideModal("ActivePartialsModal");
                await IsActiveChanged.InvokeAsync();
                return;
            }
            await Notificator.ShowError("No hemos podido desactivar el parcial");
            await Navigator.HideModal("ActivePartialsModal");
        }
    }
}