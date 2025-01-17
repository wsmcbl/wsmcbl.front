using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.MoveTeacherGuide;

public partial class UpdateTeacherGuideView : ComponentBase
{
    
    [Parameter] public string? TeacherNow { get; set; } = "N/A";
    [Parameter] public string? EnrollmentNow { get; set; } = "N/A";
    [Inject] private UpdateOfficialEnrollmentController controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Parameter] public EventCallback TeacherGuideUpdated { get; set; }
    private List<TeacherEntity> TeacherAvailable{ get; set; } = [];
    private string? enrollmentId { get; set; }
    private string? teacherId { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailable = await controller.GetNonGuidedTeacherList();
        teacherId = TeacherAvailable.FirstOrDefault()?.teacherId!;
    }

    private async Task UpdateTeacherGuide()
    {
        enrollmentId = EnrollmentNow; 
        var response = await controller.UpdateTeacherGuide(enrollmentId!, teacherId!);
        if (!response)
        {
            await Notificator.ShowError("Error", "No pudimos actualizar al maestro guía.");
            return;
        }
        
        await Notificator.ShowSuccess("Exito", "Hemos actualizado con éxito al maestro guía.");
        await Navigator.HideModal("EditTeacherGuideModal");
        await TeacherGuideUpdated.InvokeAsync();
        StateHasChanged();
    }
}