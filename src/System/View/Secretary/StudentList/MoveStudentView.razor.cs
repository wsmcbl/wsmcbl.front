using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.StudentList;

public partial class MoveStudentView : ComponentBase
{
    [Parameter] public string StudentId { get; set; } = "";
    [Parameter] public string StudentName { get; set; } = "";
    [Inject] protected MoveStudentFromEnrollmentController Controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private List<DegreeBasicDto>? Degrees { get; set; }
    private List<EnrollmentsBasicDto>? Enrollments { get; set; }
    private string NewEnrollment { get; set; } = "";
    private int EnrollmentCapacity { get; set; } = 0;
    private int EnrollmentQuantity { get; set; } = 0;
    
    protected override async Task OnParametersSetAsync()
    {
        await LoadDegrees();
    }

    private async Task LoadDegrees()
    {
        if (string.IsNullOrEmpty(StudentId))
        { 
            return;
        }
        
        Degrees = await Controller.GetDegreeBasicList(StudentId);
        StateHasChanged();
    }
    
    private void GetSelectDegreeId(ChangeEventArgs e)
    {
        var selectDegreeId = e.Value!.ToString();
        SetCurrentEnrollmentsByDegreeId(selectDegreeId);
    }
    
    private void SetCurrentEnrollmentsByDegreeId(string? selectDegreeId)
    {
        var selectedDegree = Degrees!.FirstOrDefault(e => e.degreeId == selectDegreeId);
        
        Enrollments = selectedDegree!.enrollments;
        NewEnrollment = Enrollments!.FirstOrDefault()?.enrollmentId!;
        EnrollmentCapacity = Enrollments!.FirstOrDefault()?.capacity ?? 0;
        EnrollmentQuantity = Enrollments!.FirstOrDefault()?.quantity ?? 0;
    }
    
    private void SetEnrollmentSelect(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value!.ToString();
        var enrollment = Enrollments!.FirstOrDefault(d => d.enrollmentId == selectEnrollmentId);
        
        NewEnrollment = enrollment!.enrollmentId;
        EnrollmentCapacity = enrollment.capacity;
        EnrollmentQuantity = enrollment.quantity;
    }

    private async Task UpdateStudentEnrollment()
    {
        var response = await Controller.MoveStudent(StudentId, NewEnrollment);
        if (!response)
        {
            await Notificator.ShowError("Hubo un fallo al actualizar la matrícula.");
            return;
        }
        
        await Notificator.ShowSuccess("Se ha actualizado la matrícula correctamente.");
        await LoadDegrees();
    }
}