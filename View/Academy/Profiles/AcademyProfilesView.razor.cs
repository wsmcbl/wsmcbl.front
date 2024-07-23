using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.Model.Secretary.Input;

namespace wsmcbl.front.View.Academy.Profiles;

public class AcademyProfiles : ComponentBase
{
    [Inject] protected IEnrollSudentController Controller { get; set; } = null!;
    protected List<StudentEntity>? Students;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Console.WriteLine("OBTENER DATOS DEL API");
        }
        catch (Exception ex)
        {
            // ignored
        }
    }
}