using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabStudent : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }
    [Parameter] public string SelectActive { get; set; }
    [Parameter] public string Age { get; set; }
    [Parameter] public string Sex { get; set; }
}