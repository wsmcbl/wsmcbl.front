using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.Schoolyear.TariffsView.TariffList;

namespace wsmcbl.src.View.Components.GetAcademicReport;

public partial class GetAcademicReportView : ComponentBase
{
    [Parameter] public string StudentId { get; set; } = "N/A";
    [Parameter] public string StudentName { get; set; } = "N/A";
    [Inject] private PrintReportCardStudentController Controller { get; set; } = null!;
    [Inject] private CreateSchoolyearController SchoolYearController { get; set; } = null!;
    private List<BasicSchoolyearDto> schoolYearList { get; set; } = [];
    private (string? id, string? name) SelectedSchoolYear = (null, null);

    protected override async Task OnParametersSetAsync()
    {
        schoolYearList = await SchoolYearController.GetSchoolyearList();
        SelectedSchoolYear.id = schoolYearList.First(s => s.isActive).schoolyearId;
        SelectedSchoolYear.name = schoolYearList.First(s => s.isActive).label;
    }

    private async Task GetReport()
    {
        if (!string.IsNullOrEmpty(SelectedSchoolYear.id))
        {
            await Controller.GetAcademicReportBySchoolYear(StudentId, StudentName, SelectedSchoolYear.id, SelectedSchoolYear.name!);
        }
    }
}