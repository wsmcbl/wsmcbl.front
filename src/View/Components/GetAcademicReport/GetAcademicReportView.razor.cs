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
    private (string? id, string? name) _selectedSchoolYear = (null, null);

    protected override async Task OnParametersSetAsync()
    {
        schoolYearList = await SchoolYearController.GetSchoolyearList();
        _selectedSchoolYear.id = schoolYearList.First(s => s.isActive).schoolyearId;
        _selectedSchoolYear.name = schoolYearList.First(s => s.isActive).label;
    }

    private async Task GetReport()
    {
        if (!string.IsNullOrEmpty(_selectedSchoolYear.id))
        {
            await Controller.GetAcademicReportBySchoolYear(StudentId, StudentName, _selectedSchoolYear.id, _selectedSchoolYear.name!);
        }
    }
}