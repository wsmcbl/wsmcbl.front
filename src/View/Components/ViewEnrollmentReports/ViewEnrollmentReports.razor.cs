using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Components.ViewEnrollmentReports;

public partial class ViewEnrollmentReports : BaseView
{
    [Inject] private ViewPrincipalDashboardController ViewPrincipalDashboardController { get; set; } = null!;
    [Inject] private CreateEnrollmentController CreateEnrollmentController { get; set; } = null!;
    [Inject] private AddingStudentGradesController AddingStudentGradesController { get; set; } = null!;  
    [Inject] private Navigator Navigator { get; set; } = null!;  
    private string? MyEnrollmentId { get; set; }
    private string? MyLabel { get; set; }
    private List<CombinedDegreeDto> _resultado = new();
    private List<EnrollmentListDto> MyEnrollmentList { get; set; } = new();
    private Paginator<DegreeEntity>? DegreeList { get; set; }
    private PagedRequest Request { get; set; } = new() { pageSize = 100 };
    
    //Get active partial
    private int CurrentPartialId { get; set; } = 1;
    private List<PartialEntity> Partial { get; set; } = new();

    
    
    protected override async Task OnParametersSetAsync()
    {
        MyEnrollmentList = await ViewPrincipalDashboardController.GetEnrollmentsList();
        DegreeList = await CreateEnrollmentController.GetDegreeList(Request);
        _resultado = CombineDegreesWithEnrollments(DegreeList.data, MyEnrollmentList);
        
        Partial = await AddingStudentGradesController.GetPartialList();
        CurrentPartialId = Partial.FirstOrDefault(t => t.isActive)?.partialId ?? 1;     
    }

    private List<CombinedDegreeDto> CombineDegreesWithEnrollments(
        List<DegreeEntity> degrees, 
        List<EnrollmentListDto> allEnrollments)
    {
        return degrees
            // 1. Filtrar solo degrees con quantity > 0
            .Where(degree => degree.quantity > 0)
            // 2. Ordenar por position
            .OrderBy(degree => degree.position)
            // 3. Proyectar a CombinedDegreeDto
            .Select(degree => new CombinedDegreeDto
            {
                DegreeId = degree.degreeId,
                Label = degree.label,
                SchoolYear = degree.schoolYear,
                Position = degree.position,
                EducationalLevel = degree.educationalLevel,
                
                EnrollmentList = allEnrollments
                    .Where(e => e.degreeId == degree.degreeId)
                    .ToList()
            })
            .ToList();
    }
    
    private void SetEnrollmentId(string enrollmentId, string label)
    {
        MyEnrollmentId = enrollmentId;
        MyLabel = label;
    }
    
    private async Task DownloadReport()
    {
        await ViewPrincipalDashboardController.GetReportFromEnrollment(MyEnrollmentId!, CurrentPartialId, $"Reporte de {MyLabel}");
    }

    private async Task OpenModal()
    {
        await Navigator.HideModal("DownloadReportByEnrollmentModal");
        await Navigator.ShowModal("ModalDownloadReportOfGrade");
    }
}