using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;
using wsmcbl.src.View.Secretary.EnrollStudent;
using wsmcbl.src.View.Secretary.EnrollStudent.Dto;

namespace wsmcbl.src.View.Secretary.UpdateStudentProfile;

public partial class UpdateStudentView : BaseView
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] private UpdateStudentController Controller { get; set; } = default!;
    [Inject] private Notificator Notificator { get; set; } = default!;
    private StudentFullDto Student { get; set; } = new();
    private StudentEntity? StudentEntity { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await LoadStudent();
    }

    private async Task LoadStudent()
    {
        Student = await Controller.GetStudentData(StudentId);
        StudentEntity = new StudentEntity(Student);
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

    protected override bool IsLoading()
    {
        return base.IsLoading();
    }
}