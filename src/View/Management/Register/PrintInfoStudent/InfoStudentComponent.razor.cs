using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Config;

namespace wsmcbl.src.View.Management.Register.PrintInfoStudent;

public partial class InfoStudentComponent : ComponentBase
{
    [Parameter] public RegisterDto Padron { get; set; } = new();
    [Parameter] public string? Title { get; set; }
    [Inject] public LoginController Controller { get; set; } = null!;
    private UserEntity? User { get; set; }


    protected override async Task OnInitializedAsync()
    {
         User = await Controller.getUserById();
    }
}