using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Components.StudentPasswordView;

public partial class StudentPasswordView : ComponentBase
{
    [Inject] UpdateStudentController Controller { get; set; } = null!;
    [Inject] Notificator Notificator { get; set; } = null!;
    [Inject] private IJSRuntime Js { get; set; } = null!;
    [Parameter] public string StudentId { get; set; } = "N/A";
    private StudentPassDto _student = new();
    protected override async Task OnParametersSetAsync()
    {
        await Loadstudent();
    }

    private async Task Loadstudent()
    {
        _student = await Controller.GetStudentToken(StudentId);
    }
    private async Task ResetPassword()
    {
        var des = await Notificator.ShowAlertQuestion("Advertencia", "¿Estas seguro que deseas cambiar la contraseña para el acceso a las calificaciones?", ("Si", "NO"));
        if (des)
        {
            var response = await Controller.UpdateStudentToken(StudentId);
            if (response)
            {
                await Notificator.ShowSuccess("Hemos actualizado con exito la contraseña para las calificaciones");
                await Loadstudent();
                return;
            }
            await Notificator.ShowError("Hemos tenido problemas al actualizar la contraseña para las calificaciones");
        }
    }
    private void PrintCredentials()
    {
        Js.InvokeVoidAsync("printStudentCredentials", StudentId, _student.accessToken);

    }
}