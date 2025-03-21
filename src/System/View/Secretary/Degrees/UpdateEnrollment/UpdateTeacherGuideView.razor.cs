using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Degrees.UpdateEnrollment;

public partial class UpdateTeacherGuideView : ComponentBase
{
    [Parameter] public string? TeacherNow { get; set; } = "N/A";
    [Parameter] public string? EnrollmentNow { get; set; } = "N/A";
    [Parameter] public EventCallback TeacherGuideUpdated { get; set; }
    
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private UpdateOfficialEnrollmentController controller { get; set; } = null!;
    
    private string? teacherId { get; set; }
    private string? enrollmentId { get; set; }
    private List<TeacherEntity> TeacherAvailable{ get; set; } = [];
    
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
            await Notificator.ShowError("Hubo un fallo al cambiar de maestro guía.");
            return;
        }
        
        await Notificator.ShowSuccess("Se ha cambiado el maestro guía correctamente.");
        await Navigator.HideModal("EditTeacherGuideModal");
        await TeacherGuideUpdated.InvokeAsync();
        StateHasChanged();
    }
}