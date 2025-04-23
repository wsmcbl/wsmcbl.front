using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary;

public partial class UpdateStudentComponent : ComponentBase
{
    [Parameter] public StudentEntity Student { get; set; } = null!;
    [Parameter] public bool IsChecked { get; set; }
    [Parameter] public EventCallback<bool> IsCheckedChanged { get; set; }
    [Parameter] public bool isView { get; set; }

    protected override void OnParametersSet()
    {
        if (Student.parents == null || Student.parents.Count < 2)
        {
            return;
        }
        
        Student.parents[0].sex = false;
        Student.parents[1].sex = true;
    }
    
    private string SexString
    {
        get => Student.sex ? "true" : "false";
        set => Student.sex = value == "true"; 
    }
}