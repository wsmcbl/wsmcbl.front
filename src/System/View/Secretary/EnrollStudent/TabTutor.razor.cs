using Microsoft.AspNetCore.Components;
using wsmcbl.src.Model.Secretary;

namespace wsmcbl.src.View.Secretary.EnrollStudent;

public partial class TabTutor : ComponentBase
{
    [Parameter] public StudentEntity? Student { get; set; }

    protected override void OnParametersSet()
     {
         if (Student?.parents == null || Student.parents.Count == 0)
         {
             Student!.parents = new List<StudentParent>
             {
                 new(),
                 new()
             };
         }
         else if (Student.parents.Count == 1)
         {
             Student.parents.Add(new StudentParent());
         }
         else if (Student.parents.Count > 2)
         {
             while (Student.parents.Count > 2)
             {
                 Student.parents.RemoveAt(Student.parents.Count - 1);
             }
         }
     }
}