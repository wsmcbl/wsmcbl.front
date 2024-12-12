using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class ListProfilesForEnrollmentsView : ComponentBase
{
    [Inject] protected IEnrollStudentController? Controller { get; set; }
    protected ICollection<StudentDto>? Students { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadStudents();
    }

    private async Task LoadStudents()
    {
        Students = await Controller!.GetStudents();
    }
}