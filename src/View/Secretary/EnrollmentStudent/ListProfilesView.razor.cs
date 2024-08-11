using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.EnrollmentStudent.Dto;

namespace wsmcbl.src.View.Secretary.EnrollmentStudent;

public class ListProfiles : ComponentBase
{
    [Inject] protected IEnrollSudentController Controller { get; set; }
    protected ICollection<StudentDto>? Students { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        Students = await Controller.GetStudents();
    }
}