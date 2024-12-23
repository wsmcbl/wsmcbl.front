using Microsoft.AspNetCore.Components;

namespace wsmcbl.src.View.Components.UpdateTeacherOfSubject;

public partial class UpdateTeacherOfSubjectView : ComponentBase
{
    [Parameter] public string SubjectId { get; set; } = null!;
    [Parameter] public string SubjectName { get; set; } = null!;
    [Parameter] public string EnrollmentId { get; set; } = null!;


    private Task UpdateTeacher()
    {
        throw new NotImplementedException();
    }
}