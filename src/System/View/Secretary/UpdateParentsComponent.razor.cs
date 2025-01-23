using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary;

public partial class UpdateParentsComponent : ComponentBase
{
    [Parameter] public StudentEntity Student { get; set; } = null!;

    protected override void OnParametersSet()
    {
        if (Student.parents == null)
        {
            throw new InternalException("Parent list must be not null.");
        }
    }
}