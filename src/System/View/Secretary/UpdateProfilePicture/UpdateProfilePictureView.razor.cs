using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.UpdateProfilePicture;

public partial class UpdateProfilePictureView : ComponentBase
{
    private bool IsLoading = true;
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected EnrollStudentController Controller { get; set; } = null!;
    protected StudentEntity? Student { get; private set; }
    
    protected override async Task OnParametersSetAsync()
    {
        await LoadStudent();    
    }
    
    private async Task LoadStudent()
    {
        if (StudentId != null)
        {
            var result = await Controller.GetInfoStudent(StudentId);
            Student = result.student;
            IsLoading = false;
        }
    }
}