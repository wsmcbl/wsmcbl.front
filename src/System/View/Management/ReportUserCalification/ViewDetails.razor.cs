using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Management.ReportUserCalification;

public partial class ViewDetails : BaseView
{
    [Inject] ViewPrincipalDashboardController Controller { get; set; } = null!;
    [Parameter] public List<SubjectsReportDto>? Subjects { get; set; }
    private List<SubjectsDto> ListOfSubjects { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        ListOfSubjects = await Controller.GetSubjectOfSchoolYear();
    }
}