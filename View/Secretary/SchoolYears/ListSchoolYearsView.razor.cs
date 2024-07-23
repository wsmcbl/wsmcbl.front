using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public class ListSchoolYears : ComponentBase
{
    protected List<SchoolYearDto>? SchoolYear;
    [Inject] protected CreateOfficialEnrollmentController Controller { get; set; } = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        SchoolYear = await Controller.GetSchoolYears();
    }
}