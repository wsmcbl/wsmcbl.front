using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.View.Academy.EnrollmentGuide;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Academy.Statistics;

public partial class StatisticsView : BaseView
{
    [Inject] GenerateEvaluationStatsBySectionController Controller { get; set; } = null!;
    [Inject] private ViewEnrollmentGuideController GuideController { get; set; } = null!;
    [Inject] private UpdateOfficialEnrollmentController EnrollmentController { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;

    [Parameter] public string TeacherId { get; set; } = string.Empty;
    [Parameter] public string EnrollmentLabel { get; set; } = "N/A";
    [Parameter] public int PartialId { get; set; } = 1;
    private EvaluationSumaryDto? EvaluationSumary { get; set; }
    private CualitativeDetailsDto? CualitativeDetails { get; set; }
    private List<SubjectDetailsDto>? SubjectDetails { get; set; }
    private EnrollmentDto Enrollment { get; set; } = new();
    private List<TeacherEntity> Teachers { get; set; } = new();
    
    private List<PartialEntity> Partials { get; set; } = new();
    private int PartialSelected { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        Teachers = await EnrollmentController.GetActiveTeacherList();
        Enrollment = await GuideController.GetMyEnrollmentGuide(TeacherId);
        await LoadPartials();     
        await LoadData();
    }
    
    private async Task LoadPartials()
    {
        Partials = (await AddingStudentGradesController.GetPartialList()).OrderBy(t => t.partialId).ToList();
        PartialSelected = Partials.FirstOrDefault(t => t.isActive)?.partialId ?? 1;
    }

    private async Task LoadData()
    {
        EvaluationSumary = await Controller.GetStatsSummary(TeacherId, PartialSelected);
        CualitativeDetails = await Controller.GetCualitativeDetails(TeacherId, PartialSelected);
        SubjectDetails = await Controller.GetSubjectDetails(TeacherId, PartialSelected);
    }

    private async Task OnPartialSelected(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int newPartialId))
        {
            try
            {
                PartialSelected = newPartialId;
                await LoadData();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
    private async Task DownloadPartialReport()
    {
        var partialLabel = Partials.FirstOrDefault(t => t.partialId == PartialSelected)?.label ?? "N/A";
        await GuideController.GetStatisticsDocument(TeacherId, PartialSelected, partialLabel, EnrollmentLabel);
    }

}