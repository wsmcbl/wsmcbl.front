using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;
using wsmcbl.src.View.Shared;

namespace wsmcbl.src.View.Secretary.EnrollmentStudent;

public class ListProfiles : ComponentBase
{
    [Inject] protected IEnrollSudentController Controller { get; set; }
    [Inject] protected AlertService AlertService { get; set; }
    protected List<StudentDto>? Students;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Students = await Controller.GetStudents();
        }
        catch (Exception ex)
        {
            AlertService.AlertError("Error del servidor", $"{ex}");
        }
    }
    
    
}
    

    