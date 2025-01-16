using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.MoveTeacherGuide;

namespace wsmcbl.src.View.Components.UpdateTeacherOfSubject;

public partial class UpdateTeacherOfSubjectView : ComponentBase
{
    [Parameter] public string SubjectId { get; set; } = null!;
    [Parameter] public string SubjectName { get; set; } = null!;
    [Parameter] public string EnrollmentId { get; set; } = null!;
    [Parameter] public EventCallback TeacherSubjectUpdated { get; set; }

    [Inject] private MoveTeacherGuideFromEnrollmentController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;

    private string? teacherId {get;set;}
    private List<TeacherEntity> TeacherAvailableList = [];

    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailableList = await Controller.GetActiveTeachers();
    }


    private async Task UpdateTeacher()
    {
        var response = await Controller.UpdateTeacherSubject(EnrollmentId, SubjectId, teacherId!);
        if (!response)
        {
            await Notificator.ShowError("Error", "No pudimos asignar al docente.");
            return;
        }
        
        await Notificator.ShowSuccess("Exito", "Hemos asignado al nuevo docente.");
        await TeacherSubjectUpdated.InvokeAsync();
        await Navigator.HideModal("UpdateTeacherSubject");
        StateHasChanged();
    }

    private Task GetTeacherIdSelect(ChangeEventArgs e)
    {
        var selectTeacherId = e.Value!.ToString();

        if (selectTeacherId != null)
        {
            teacherId = selectTeacherId;
        }

        return Task.CompletedTask;
    }
}
