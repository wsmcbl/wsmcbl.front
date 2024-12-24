using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
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
    private List<TeacherNoGuideDto> TeacherAvailable{ get; set; } = [];
    private ChangeTeacherDto NewTeacher { get; set; } = new ChangeTeacherDto();
    private string? NewTeacherId { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailable = await Controller.GetTeacherNoGuide();
        NewTeacher.newTeacherId = TeacherAvailable.FirstOrDefault()?.teacherId!;
    }

    private async Task UpdateTeacherG()
    {
        if (NewTeacherId != null)
        {
            NewTeacher.newTeacherId = NewTeacherId;
        }
        NewTeacher.enrollmentId = EnrollmentNow; 
        var response = await Controller.UpdateTeacherGuide(NewTeacher);

        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado con exito al maestro guia");
            await Navigator.HideModal("EditTeacherGuideModal");
            await TeacherGuideUpdated.InvokeAsync();
            StateHasChanged();
            return;
        }
        await Notificator.ShowError("Error", "No pudimos actualizar al maestro guia.");
    }
}