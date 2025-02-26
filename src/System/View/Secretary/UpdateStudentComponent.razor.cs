using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary;

public partial class UpdateStudentComponent : ComponentBase
{
    [Parameter] public StudentEntity Student { get; set; } = null!;
    [Parameter] public bool isEditing { get; set; } = false;

    protected override void OnParametersSet()
    {
        if (Student.parents == null || Student.parents.Count < 2)
        {
            return;
        }
        
        Student.parents[0].sex = false;
        Student.parents[1].sex = true;
    }
}