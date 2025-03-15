using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Management.PartialsGradeRecording;

public partial class ActivePartialsGradeComponent : ComponentBase
{
    [Inject] public EnablePartialGradeRecordingController Controller { get; set; } = default!;
    [Inject] public Notificator Notificator { get; set; } = default!;
    [Inject] public Navigator Navigator { get; set; } = default!;
    [Parameter] public int PartialId {get; set;}
    [Parameter] public string? PartialName {get; set;}
    [Parameter] public string? Semester {get; set;}
    [Parameter] public string? DateRange {get; set;}
    private bool Enabled {get; set;} = true;
    
    
    
    private DateTime DeadLineMax { get; set; } = DateTime.UtcNow.AddDays(15);
    private DateTime DeadLineMin { get; set; } = DateTime.UtcNow.AddHours(1);
    private DateTime DeadLine { get; set; } = DateTime.UtcNow.AddHours(2);


    private string FormatDateTime(DateTime date) => date.ToString("yyyy-MM-ddTHH:mm");

    private async Task ActivePartials()
    {
        var formattedDeadline = DeadLine.ToString("yyyy-MM-ddTHH:mm:ss.fffffff");
        var response = await Controller.ActiveGradesRecording(PartialId, Enabled, formattedDeadline );
        if (response)
        {
            await Notificator.ShowSuccess("Exito",
                "Hemos activado el registro de calificaciones con exito, ahora los docentes pueden guardar las calificaciones de los estudiantes.");
            await Navigator.HideModal("ActivePartialsGradeModal");
            return;
        }
        
        await Notificator.ShowError("Error",
            "No hemos podido activar el registro para las calificaciones, intentelo mas tarde.");
    }
}