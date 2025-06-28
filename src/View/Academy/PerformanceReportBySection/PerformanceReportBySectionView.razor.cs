using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.PerformanceReportBySection;

public partial class PerformanceReportBySectionView : BaseView
{
    [Inject] private GeneratePerformanceReportBySection generatePerformanceReportBySectionController { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Parameter] public List<SubjectEntity> subjectList { get; set; } = new();
    [Parameter] public string EnrollmentName { get; set; } = "N/A";
    [Parameter] public string? TeacherId { get; set; }
    [Parameter] public int PartialId { get; set; }
    [Parameter] public bool IsGuide { get; set; }
    private List<StudentsGradeSumaryDto>? StudentsList { get; set; }
    private int PartialSelected { get; set; }
    private List<PartialEntity> Partials { get; set; } = new();


    protected override async Task OnParametersSetAsync()
    {
        if (!IsGuide)
        {
            return;
        }

        await LoadPartials();
        await LoadStudent();
        OrderStudentGradeList();
    }
    private async Task LoadPartials()
    {
        Partials = (await AddingStudentGradesController.GetPartialList()).OrderBy(t => t.partialId).ToList();
        PartialSelected = Partials.FirstOrDefault(t => t.isActive)?.partialId ?? 1;
    }
    private async Task LoadStudent()
    {
        if (!string.IsNullOrEmpty(TeacherId))
        {
            StudentsList = await generatePerformanceReportBySectionController.GetStudentsGradeSummary(TeacherId, PartialId);
        }
    }
    private void OrderStudentGradeList()
    {
        var list = subjectList.Select(e => e.subjectId).ToList();
        foreach (var item in StudentsList!)
        {
            item.gradeList = item.gradeList.OrderBy(e => list.IndexOf(e.subjectId)).ToList();
        }
    }
    private async Task OnPartialSelected(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int newPartialId))
        {
            try
            {
                PartialSelected = newPartialId;
                StudentsList = await generatePerformanceReportBySectionController.GetStudentsGradeSummary(TeacherId!, PartialSelected);
                if (StudentsList.Count == 0)
                {
                    PartialSelected = Partials.FirstOrDefault(t => t.isActive)?.partialId ?? 1;
                    StudentsList = await generatePerformanceReportBySectionController.GetStudentsGradeSummary(TeacherId!, PartialSelected);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
    private async Task DownloadReport()
    {
        if (TeacherId != null && !string.IsNullOrEmpty(EnrollmentName))
        {
            var partialLabel = Partials.FirstOrDefault(t => t.partialId == PartialSelected)?.label ?? "N/A";
            await generatePerformanceReportBySectionController.GetEnrollmentGradeByTeacherXlsx(TeacherId, PartialSelected, partialLabel, EnrollmentName);
        }
    }
    protected override bool IsLoading()
    {
        return StudentsList == null || Partials.Count < 4 || IsGuide == false;
    }
}