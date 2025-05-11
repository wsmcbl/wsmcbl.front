using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.PerformanceReportBySection;

public partial class PerformanceReportBySectionView : BaseView
{
    [Inject] GeneratePerformanceReportBySection Controller {get; set;} = null!;
    [Parameter] public List<SubjectEntity> subjectList { get; set; } = new();
    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public int PartialId { get; set; }
    [Parameter] public bool IsGuide { get; set; }
    private List<StudentsGradeSumaryDto>? StudentsList { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        if (!IsGuide)
        {      
            return;
        }
        
        await LoadStudent();
        orderStudentGradeList();
    }

    private void orderStudentGradeList()
    {
        var list = subjectList.Select(e => e.subjectId).ToList();
        foreach (var item in StudentsList!)
        {
            item.gradeList = item.gradeList.OrderBy(e => list.IndexOf(e.subjectId)).ToList();
        }
    }

    private async Task LoadStudent()
    {
        if (!string.IsNullOrEmpty(TeacherId))
        {
            StudentsList = await Controller.GetStudentsGradeSummary(TeacherId, PartialId);
        }
    }

    protected override bool IsLoading()
    {
        return StudentsList == null;
    }
    
}