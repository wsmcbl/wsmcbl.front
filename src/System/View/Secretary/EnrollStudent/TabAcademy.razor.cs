using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabAcademy : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }
    [Parameter] public int discountId { get; set; }
    [Parameter] public List<DegreeBasicDto>? Degrees { get; set; }
    private List<EnrollmentsBasicDto>? CurrentEnrollments { get; set; }
    private int CurrentEnrollmentCapacity { get; set; }
    private int CurrentEnrollmentQuantity { get; set; }
    private string EnrollmentIdSelected { get; set; }
    

    protected override async Task OnParametersSetAsync()
    {
        if (Degrees.Any())
        {
            CurrentEnrollments = Degrees
                .Where(t => t.enrollments != null && t.enrollments.Any())
                .Select(t => t.enrollments)
                .FirstOrDefault(); 
            EnrollmentIdSelected  = CurrentEnrollments.FirstOrDefault()?.enrollmentId ?? "No asignado";
        }
        else
        {
            CurrentEnrollments = new List<EnrollmentsBasicDto> { new() };       
        }
    }
    
    private void SetDiscount(int value)
    {
        discountId = value;
    }
    
    private void GetSelectDegreeId(ChangeEventArgs e)
    {
        var selectDegreeId = e.Value.ToString();
        setCurrentEnrollmentsByDegreeId(selectDegreeId);
    }
    
    private void setCurrentEnrollmentsByDegreeId(string? selectDegreeId)
    {
        var selectedDegree = Degrees.FirstOrDefault(e => e.degreeId == selectDegreeId);
        CurrentEnrollments = selectedDegree.enrollments;
    }
    
    private void GetSection(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value.ToString();
        var enrollment = CurrentEnrollments.FirstOrDefault(e => e.enrollmentId == selectEnrollmentId);
        
        EnrollmentIdSelected = enrollment.enrollmentId;
        CurrentEnrollmentCapacity = enrollment.capacity;
        CurrentEnrollmentQuantity = enrollment.quantity;
    }
    
    
    
}