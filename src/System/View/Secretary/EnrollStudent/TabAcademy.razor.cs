using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;
namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabAcademy : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }
    
    [Parameter] public int DiscountId { get; set; }
    [Parameter] public EventCallback<int> DiscountIdChanged { get; set; }
    
    [Parameter] public List<DegreeBasicDto>? Degrees { get; set; }
    [Parameter] public EventCallback<string> EnrollmentIdSelectedChanged { get; set; }
    
    [Parameter] public string EnrollmentIdSelected { get; set; }
    private List<EnrollmentsBasicDto>? CurrentEnrollments { get; set; }
    private int CurrentEnrollmentCapacity { get; set; }
    private int CurrentEnrollmentQuantity { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        if (!Degrees.Any())
        {
            CurrentEnrollments = new List<EnrollmentsBasicDto> { new() }; 
            return;
        }
        
        CurrentEnrollments = Degrees
            .Where(t => t.enrollments != null && t.enrollments.Any())
            .Select(t => t.enrollments)
            .FirstOrDefault();
                
        var selectedId = CurrentEnrollments.FirstOrDefault()?.enrollmentId ?? "No asignado";
        CurrentEnrollmentCapacity = CurrentEnrollments.FirstOrDefault()?.capacity ?? 0;
        CurrentEnrollmentQuantity = CurrentEnrollments.FirstOrDefault()?.quantity ?? 0;
            
        if (EnrollmentIdSelected != selectedId)
        {
            EnrollmentIdSelected = selectedId;
            await EnrollmentIdSelectedChanged.InvokeAsync(EnrollmentIdSelected);
        }
    }
    
    private async Task SetDiscount(int value)
    {
        DiscountId = value;
        await DiscountIdChanged.InvokeAsync(DiscountId);
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
        EnrollmentIdSelected = CurrentEnrollments.FirstOrDefault()?.enrollmentId;
    }
    
    private void SetEnrollmentSelect(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value.ToString();
        var enrollment = CurrentEnrollments.FirstOrDefault(e => e.enrollmentId == selectEnrollmentId);
        
        EnrollmentIdSelected = enrollment.enrollmentId;
        CurrentEnrollmentCapacity = enrollment.capacity;
        CurrentEnrollmentQuantity = enrollment.quantity;
    }
    
    
    
}