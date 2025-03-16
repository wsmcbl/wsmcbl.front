using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public class ListSchoolYears : ComponentBase
{
    protected List<SchoolYearDto>? SchoolYear;
    [Inject] protected CreateSchoolyearController Controller { get; set; } = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        SchoolYear = await Controller.GetSchoolyearList();
    }
}