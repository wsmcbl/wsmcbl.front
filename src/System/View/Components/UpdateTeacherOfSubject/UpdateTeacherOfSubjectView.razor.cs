using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.MoveTeacherGuide;

namespace wsmcbl.src.View.Components.UpdateTeacherOfSubject;

public partial class UpdateTeacherOfSubjectView : ComponentBase
{
    [Parameter] public string SubjectId { get; set; } = null!;
    [Parameter] public string SubjectName { get; set; } = null!;
    [Parameter] public string EnrollmentId { get; set; } = null!;
    [Inject] private MoveTeacherGuideFromEnrollmentController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    
    private TeacherSubjectDto NewTeacherSubject = new();
    private List<TeacherNoGuideDto> TeacherAvailableList = [];
    [Parameter] public EventCallback TeacherSubjectUpdated { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        TeacherAvailableList = await Controller.GetActiveTeachers();
    }


    private async Task UpdateTeacher()
    {
        NewTeacherSubject.subjectId = SubjectId;
        NewTeacherSubject.enrollmentId = EnrollmentId;
        
        var response = await Controller.UpdateTeacherSubject(NewTeacherSubject);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos asignado al nuevo maestro");
            await TeacherSubjectUpdated.InvokeAsync();
            await Navigator.HideModal("UpdateTeacherSubject");
            StateHasChanged();
            return;
        }

        await Notificator.ShowError("Error", "No pudimos asignar al maestro");
    }

    private Task GetTeacherIdSelect(ChangeEventArgs e)
    {
        var selectTeacherId = e.Value!.ToString();
        if (selectTeacherId != null) NewTeacherSubject.teacherId = selectTeacherId;
        return Task.CompletedTask;
    }
}