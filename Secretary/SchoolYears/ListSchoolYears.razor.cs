using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controllers;
using wsmcbl.front.dto.input;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Secretary.SchoolYears;

public class ListSchoolYears_razor : ComponentBase
{
    protected List<SchoolYearDto>? SchoolYear;
    [Inject] protected SecretaryController controller { get; set; } = null!;
    
    protected override async Task OnParametersSetAsync()
    {
        SchoolYear = await controller.GetSchoolYears();
    }
}