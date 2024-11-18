using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Secretary.SchoolYears;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabStudent : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }
    [Parameter] public bool SelectRepeat { get; set; }
    [Parameter] public EventCallback<bool> SelectRepeatChanged { get; set; }
    [Parameter] public int Age { get; set; }
    [Parameter] public string Sex { get; set; }
    
    [Parameter] public bool IsRepeating { get; set; }
    
    protected override async Task OnParametersSetAsync()
    {
        if (Student != null)
        {
            Age = Student.birthday.AgeCompute();
            Sex = Student.sex ? "true" : "false";
        }
        
        if (Student?.parents != null && Student.parents.Count >= 2)
        {
            Student.parents[0].sex = false;
            Student.parents[1].sex = true;
        }
    }
    
    private void UpdateRepeatSelection(bool value)
    {
        SelectRepeat = value;
        SelectRepeatChanged.InvokeAsync(value);
    }
    
}