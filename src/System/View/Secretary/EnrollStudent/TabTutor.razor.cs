using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabTutor : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }
}