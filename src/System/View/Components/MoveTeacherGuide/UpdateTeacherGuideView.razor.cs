using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.MoveTeacherGuide;

public partial class UpdateTeacherGuideView : ComponentBase
{
    
    [Parameter] public string? TeacherNow { get; set; } = "N/A";
    [Parameter] public string? EnrollmentNow { get; set; } = "N/A";
    [Inject] MoveTeacherGuideFromEnrollmentController Controller { get; set; } = null!;
    [Inject] Notificator Notificator { get; set; } = null!;
    [Inject] Navigator Navigator { get; set; } = null!;
    [Parameter] public EventCallback TeacherGuideUpdated { get; set; }
    private List<TeacherEntity> TeacherAvailable{ get; set; } = [];
    public string? enrollmentId { get; set; }
    public string? teacherId { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailable = await Controller.GetTeacherNoGuide();
        teacherId = TeacherAvailable.FirstOrDefault()?.teacherId!;
    }

    private async Task UpdateTeacherGuide()
    {
        enrollmentId = EnrollmentNow; 
        var response = await Controller.UpdateTeacherGuide(enrollmentId!, teacherId!);
        if (response)
        {
            await Notificator.ShowError("Error", "No pudimos actualizar al maestro guia.");
            return;
        }
        
        await Notificator.ShowSuccess("Exito", "Hemos actualizado con exito al maestro guia");
        await Navigator.HideModal("EditTeacherGuideModal");
        await TeacherGuideUpdated.InvokeAsync();
        StateHasChanged();
    }
}