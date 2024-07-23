using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.View.Academy.Profiles;

public class AcademyProfiles : ComponentBase
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