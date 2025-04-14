using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Components.DashbardStudent;

public partial class DashboardStudentView : ComponentBase
{
    [Parameter] public StudentEntity Student { get; set; } = new();
    [Inject] private IJSRuntime JSRuntime { get; set; } = null!;

    private string GetProfilePicture()
    {
        if (!string.IsNullOrEmpty(Student.profilePicture))
        {
            return $"data:image/png;base64,{Student.profilePicture}";
        }

        return "/img/Placeholder/Man.png";
    }
}