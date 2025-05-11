using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.UpdateStudentProfile;

public partial class UpdateStudentView : BaseView
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private UpdateStudentController Controller { get; set; } = null!;
    private StudentEntity? student { get; set; }
    private bool generateToken { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        student = await Controller.GetStudentById(StudentId);
    }

    private async Task UpdateStudent()
    {
        if (student == null)
        {
            throw new InternalException("StudentEntity must be not null.");
        }

        student.studentId = StudentId;
        var response = await Controller.UpdateStudentData(student, generateToken);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha actualizado el estudiante correctamente.");
            return;
        }
        
        await Notificator.ShowError( "Hubo un fallo al actualizar el estudiante.");
    }

    protected override bool IsLoading()
    {
        return student == null;
    }
}