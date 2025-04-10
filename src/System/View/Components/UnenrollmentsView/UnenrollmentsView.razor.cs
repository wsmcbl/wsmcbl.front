using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Components.UnenrollmentsView;

public partial class UnenrollmentsView : BaseView
{
    [Inject] UnenrollmentController Controller { get; set; } = null!;
    [Inject] GetSchoolYearServices SchoolYearService { get; set; } = null!;
    private List<UnenrollmentBasicDto>? StudentsList { get; set; }
    private Dictionary<string, string>? SchoolYearLabels { get; set; }


    protected override async Task OnParametersSetAsync()
    {
        StudentsList = await Controller.GetStudents();
        if (StudentsList != null)
        {
            var uniqueSchoolYearIds = StudentsList.Select(s => s.schoolyearId).Distinct().ToList();
            SchoolYearLabels = await SchoolYearService.GetSchoolYearLabelsBatch(uniqueSchoolYearIds);
        }
    }
    
    protected override bool IsLoading()
    {
        return StudentsList == null;
    }
}