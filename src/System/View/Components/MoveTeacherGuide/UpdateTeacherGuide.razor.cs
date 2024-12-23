using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.MoveTeacherGuide;

public partial class UpdateTeacherGuide : ComponentBase
{
    
    [Parameter] public string? TeacherNow { get; set; } = "N/A";
    [Parameter] public string? EnrollmentNow { get; set; } = "N/A";
    [Inject] MoveTeacherGuideFromEnrollmentController Controller { get; set; } = null!;
    [Inject] Notificator Notificator { get; set; } = null!;
    private List<TeacherNoGuideDto> TeacherAvailable{ get; set; } = [];
    private ChangeTeacherDto NewTeacher { get; set; } = new ChangeTeacherDto();
    private string NewTeacherId { get; set; } = string.Empty;

    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailable = await Controller.GetTeacherNoGuide();
    }

    private async Task UpdateTeacherG()
    {
        NewTeacher.newTeacherId = NewTeacherId;
        NewTeacher.enrollmentId = EnrollmentNow; 
        var response = await Controller.UpdateTeacherGuide(NewTeacher);

        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado con exito al maestro guia");
            return;
        }
        await Notificator.ShowError("Error", "No pudimos actualizar al maestro guia.");
    }
}