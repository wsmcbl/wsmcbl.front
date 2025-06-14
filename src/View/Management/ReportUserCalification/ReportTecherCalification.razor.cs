using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Management.ReportUserCalification;

public partial class ReportTecherCalification : BaseView
{
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    [Inject] Navigator Navigator { get; set; } = null!;
    private List<TeacherReportDto>? teacherList { get; set; }
    private List<SubjectsReportDto> toDetailsView { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        teacherList = await Controller.GetTeacherGradeReports();
    }
    
    protected override bool IsLoading()
    {
        return teacherList == null;
    }

    private async Task ViewDetails(List<SubjectsReportDto> item)
    {
        toDetailsView = item;
        await Navigator.ShowModal("DetailReportTeacherModal");
    }
}