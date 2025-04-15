using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace wsmcbl.src.View.Components.StudentPasswordView;

public partial class StudentPasswordView : ComponentBase
{
    [Inject] private IJSRuntime Js { get; set; } = null!;
    [Parameter] public string StudentId { get; set; } = null!;
    [Parameter] public string TempPassword { get; set; } = "856295";

    private Task ResetPassword()
    {
        return Task.CompletedTask;
        
    }

    private void printCredentials()
    {
        Js.InvokeVoidAsync("printStudentCredentials", StudentId, TempPassword);

    }
}