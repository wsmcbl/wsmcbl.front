using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
using wsmcbl.src.View.Secretary.Profiles;

namespace wsmcbl.src.View.Components.MoveStudent;

public partial class MoveStudentView : ComponentBase
{
    [Parameter] public string StudentId { get; set; } = "";
    [Parameter] public string EnrollmentName { get; set; } = "";
    [Inject] protected EnrollStudentController ControllerEnrollment { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private List<DegreeBasicDto>? Degrees { get; set; }
    private List<EnrollmentsBasicDto>? Enrollments { get; set; }
    private string NewEnrollment { get; set; } = "";
    private int EnrollmentCapacity { get; set; } = 0;
    private int EnrollmentQuantity { get; set; } = 0;



    protected override Task OnParametersSetAsync()
    {
        LoadDegrees();
        return Task.CompletedTask;
    }

    private async void LoadDegrees()
    {
        Degrees = await ControllerEnrollment.GetDegreeBasicList();
    }
    
    private async Task GetSelectDegreeId(ChangeEventArgs e)
    {
        var selectDegreeId = e.Value!.ToString();
        await SetCurrentEnrollmentsByDegreeId(selectDegreeId);
    }
    
    private Task SetCurrentEnrollmentsByDegreeId(string? selectDegreeId)
    {
        var selectedDegree = Degrees!.FirstOrDefault(e => e.degreeId == selectDegreeId);
        
        Enrollments = selectedDegree!.enrollments;
        NewEnrollment = Enrollments!.FirstOrDefault()?.enrollmentId!;
        EnrollmentCapacity = Enrollments!.FirstOrDefault()?.capacity ?? 0;
        EnrollmentQuantity = Enrollments!.FirstOrDefault()?.quantity ?? 0;
        return Task.CompletedTask;
    }
    
    private Task SetEnrollmentSelect(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value!.ToString();
        var enrollment = Enrollments!.FirstOrDefault(d => d.enrollmentId == selectEnrollmentId);
        
        NewEnrollment = enrollment!.enrollmentId;
        EnrollmentCapacity = enrollment.capacity;
        EnrollmentQuantity = enrollment.quantity;
        return Task.CompletedTask;
    }

    private async Task UpdateStudentEnrollment()
    {
        var response = await ControllerEnrollment.UpdateEnrollmet(StudentId, NewEnrollment);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Hemos actualizado la matricula.");
            LoadDegrees();
            StateHasChanged();
            return;
        }
        
        await Notificator.ShowError("Error", "No pudimos actualizar la matricula.");
    }
}