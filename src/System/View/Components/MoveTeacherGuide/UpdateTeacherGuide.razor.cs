using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.MoveTeacherGuide;

public partial class UpdateTeacherGuide : ComponentBase
{
    
    [Parameter] public string? TeacherNow { get; set; } = "N/A";
    [Parameter] public string? Enrollment { get; set; } = "N/A";
    [Inject] MoveTeacherGuideFromEnrollmentController Controller { get; set; } = null!;
    [Inject] Notificator Notificator { get; set; } = null!;
    private List<TeacherNoGuideDto> TeacherAvailable{ get; set; } = [];
    private ChangeTeacherDto Teacher { get; set; } = new ChangeTeacherDto();

    private string NewTeacher { get; set; } = string.Empty;

    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailable = await Controller.GetTeacherNoGuide();
    }

    private async Task UpdateTeacherG()
    {
        Teacher.newTeacherId = NewTeacher;
        Teacher.enrollmentId = Enrollment; 
        var response = await Controller.UpdateTeacherGuide(Teacher);

        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado con exito al maestro guia");
            return;
        }
        
        await Notificator.ShowError("Error", "No pudimos actualizar al maestro guia.");
    }
}