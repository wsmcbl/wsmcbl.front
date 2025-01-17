using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.EditProfiles;

public partial class EditProfileStudent : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] private UpdateStudentController Controller { get; set; } = default!;
    [Inject] private Notificator Notificator { get; set; } = default!;
    private StudentFullDto Student { get; set; } = new();
    private StudentEntity? StudentEntity { get; set; }
    private bool IsLoading { get; set; } = true;


    protected override async Task OnParametersSetAsync()
    {
        await LoadStudent();
    }

    private async Task LoadStudent()
    {
        Student = await Controller.GetStudentData(StudentId);
        StudentEntity = new StudentEntity(Student);
        IsLoading = false;
    }

    private async Task UpdateStudent()
    {
        var response = await Controller.UpdateStudentData(StudentEntity);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Los datos han sido actualizados con exito");
            return;
        }
        await Notificator.ShowError("Error", "No pudimos actualizar los datos");
    }
}