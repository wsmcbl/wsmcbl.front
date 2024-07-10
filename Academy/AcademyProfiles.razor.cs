using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controllers;
using wsmcbl.front.Models.Secretary.Input;

namespace wsmcbl.front.Academy;

public class AcademyProfiles_razor : ComponentBase
{
    [Inject] protected AcademyController controller { get; set; } = null!;
    protected List<StudentEntity>? Students;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Students = await controller.GetStudents();
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
}