using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Academy.EnrollmentGuide;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.PerformanceReportBySection;

public partial class PerformanceReportBySectionView : BaseView
{
    [Inject] GeneratePerformanceReportBySection Controller {get; set;} = null!;
    [Parameter] public List<SubjectDto> subjectList { get; set; } = new();
    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public int PartialId { get; set; }
    [Parameter] public bool Isguide { get; set; }
    private List<StudentsGradeSumaryDto>? StudentsList { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        if (Isguide)
        {      
            await LoadStudent();
        }
    }

    private async Task LoadStudent()
    {
        if (!string.IsNullOrEmpty(TeacherId))
        {
            StudentsList = await Controller.GetStudentsGradeSumary(TeacherId, PartialId);
        }
    }

    protected override bool IsLoading()
    {
        return StudentsList == null;
    }
    
}