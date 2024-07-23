using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.dto.input;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.View.Secretary.SchoolYears;

public class ListSchoolYears : ComponentBase
{
    protected List<SchoolYearDto>? SchoolYear;
    [Inject] protected CreateOfficialEnrollmentController controller { get; set; } = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        SchoolYear = await controller.GetSchoolYears();
    }
}