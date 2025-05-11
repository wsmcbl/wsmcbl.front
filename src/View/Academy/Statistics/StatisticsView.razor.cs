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

    [Parameter] public string TeacherId { get; set; } = string.Empty;
    [Parameter] public int PartialId { get; set; } = 1;
    private EvaluationSumaryDto? EvaluationSumary { get; set; }
    private CualitativeDetailsDto? CualitativeDetails { get; set; }
    private List<SubjectDetailsDto>? SubjectDetails { get; set; }
    private EnrollmentDto Enrollment { get; set; } = new();
    private List<TeacherEntity> Teachers { get; set; } = new();

    

    
    
    protected override async Task OnParametersSetAsync()
    {
        Teachers = await EnrollmentController.GetActiveTeacherList();
        Enrollment = await GuideController.GetMyEnrollmentGuide(TeacherId);
        EvaluationSumary = await Controller.GetStatsSummary(TeacherId, PartialId);
        CualitativeDetails = await Controller.GetCualitativeDetails(TeacherId, PartialId);
        SubjectDetails = await Controller.GetSubjectDetails(TeacherId, PartialId);
    }
}