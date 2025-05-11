using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabAcademy : ComponentBase
{
    [Parameter] public StudentEntity Student { get; set; } = null!;
    [Parameter] public List<DegreeBasicDto> DegreeList { get; set; } = null!;

    [Parameter] public int DiscountId { get; set; }
    [Parameter] public EventCallback<int> DiscountIdChanged { get; set; }

    [Parameter] public string? EnrollmentIdSelected { get; set; }
    [Parameter] public EventCallback<string> EnrollmentIdSelectedChanged { get; set; }

    [Parameter] public bool SelectRepeat { get; set; }
    [Parameter] public EventCallback<bool> SelectRepeatChanged { get; set; }

    private int CurrentEnrollmentCapacity { get; set; }
    private int CurrentEnrollmentQuantity { get; set; }
    private List<EnrollmentsBasicDto>? CurrentEnrollments { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        if (DegreeList.Count == 0)
        {
            CurrentEnrollments = [new EnrollmentsBasicDto()];
            return;
        }

        CurrentEnrollments = DegreeList
            .Where(t => t.enrollments != null && t.enrollments.Count != 0)
            .Select(t => t.enrollments)
            .FirstOrDefault();

        var selectedId = CurrentEnrollments!.FirstOrDefault()?.enrollmentId ?? "No asignado";

        CurrentEnrollmentCapacity = CurrentEnrollments!.FirstOrDefault()?.capacity ?? 0;
        CurrentEnrollmentQuantity = CurrentEnrollments!.FirstOrDefault()?.quantity ?? 0;

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

    private async Task GetSelectDegreeId(ChangeEventArgs e)
    {
        var selectDegreeId = e.Value!.ToString();
        await setCurrentEnrollmentsByDegreeId(selectDegreeId);
    }

    private async Task SetEnrollmentSelect(ChangeEventArgs e)
    {
        var selectEnrollmentId = e.Value!.ToString();
        var enrollment = CurrentEnrollments!.FirstOrDefault(d => d.enrollmentId == selectEnrollmentId);

        EnrollmentIdSelected = enrollment!.enrollmentId;
        await EnrollmentIdSelectedChanged.InvokeAsync(EnrollmentIdSelected);
        CurrentEnrollmentCapacity = enrollment.capacity;
        CurrentEnrollmentQuantity = enrollment.quantity;
    }

    private async Task setCurrentEnrollmentsByDegreeId(string? selectDegreeId)
    {
        var selectedDegree = DegreeList.FirstOrDefault(e => e.degreeId == selectDegreeId);

        CurrentEnrollments = selectedDegree!.enrollments;
        EnrollmentIdSelected = CurrentEnrollments!.FirstOrDefault()?.enrollmentId!;

        await EnrollmentIdSelectedChanged.InvokeAsync(EnrollmentIdSelected);
        CurrentEnrollmentCapacity = CurrentEnrollments!.FirstOrDefault()?.capacity ?? 0;
        CurrentEnrollmentQuantity = CurrentEnrollments!.FirstOrDefault()?.quantity ?? 0;
    }

    private void UpdateRepeatSelection(bool value)
    {
        SelectRepeat = value;
        SelectRepeatChanged.InvokeAsync(value);
    }
}