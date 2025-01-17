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
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private UpdateStudentController Controller { get; set; } = null!;
    private StudentEntity? student { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        student = await Controller.GetStudentById(StudentId);
    }

    private async Task UpdateStudent()
    {
        var response = await Controller.UpdateStudentData(student);
        if (response)
        {
            await Notificator.ShowSuccess("Exito", "Los datos han sido actualizados con exito");
            return;
        }
        
        await Notificator.ShowError("Error", "No pudimos actualizar los datos");
    }

    protected override bool IsLoading()
    {
        return student == null;
    }
}