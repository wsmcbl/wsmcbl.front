using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.EditProfiles;

public partial class EditProfileStudent : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] private EditStudentController Controller { get; set; } = default!;
    private StudentFullDto Student { get; set; } = new();
    private bool IsLoading { get; set; } = true;


    protected override async Task OnParametersSetAsync()
    {
        await LoadStudent();
    }

    private async Task LoadStudent()
    {
        //Esperando Fix en la API
        Student = await Controller.GetStudentData(StudentId);
        IsLoading = false;
    }
}