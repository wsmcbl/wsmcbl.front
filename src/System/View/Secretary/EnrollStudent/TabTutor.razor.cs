using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabTutor : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }

    protected override async Task OnParametersSetAsync()
     {
         if (Student?.parents == null || !Student.parents.Any())
         {
             Student.parents = new List<StudentParent>();
             Student.parents.Add(new StudentParent());
             Student.parents.Add(new StudentParent());
         }
     }
}