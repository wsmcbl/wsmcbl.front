using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.Schoolyear;

namespace wsmcbl.src.View.Components.ViewEnrollmentReports;

public partial class ViewEnrollmentReports : BaseView
{
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    [Inject] protected CreateEnrollmentController controller { get; set; } = null!;
    private string? MyEnrollmentId { get; set; }
    private string? MyLabel { get; set; }
    private List<CombinedDegreeDto> _resultado = new();
    private List<EnrollmentListDto> MyEnrollmentList { get; set; } = new();
    private Paginator<DegreeEntity>? DegreeList { get; set; }
    private PagedRequest Request { get; set; } = new() { pageSize = 100 };
    
    protected override async Task OnParametersSetAsync()
    {
        MyEnrollmentList = await Controller.GetEnrollmentsList();
        DegreeList = await controller.GetDegreeList(Request);
        _resultado = CombineDegreesWithEnrollments(DegreeList.data, MyEnrollmentList);
    }

    public List<CombinedDegreeDto> CombineDegreesWithEnrollments(
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
    
    private async Task DowloadReport()
    {
        await Controller.GetReportFromEnrollment(MyEnrollmentId!, 1, $"Reporte de {MyLabel}");
    }
}