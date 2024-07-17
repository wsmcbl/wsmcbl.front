using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controllers;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Academy.CreateOfficialEnrollmentBySection;

public  class ConfigSchoolYear_razor : ComponentBase
{
    [Inject] protected SecretaryController Controller { get; set; } = null!;
    [Parameter] public string? SchoolYearId { get; set; }
    
    protected List<GradeEntity>? ListGrade;
    protected List<Subject>? ListSubject;

    protected override async Task OnParametersSetAsync()
    {
        ListGrade = await Controller.GetGrades();
        ListSubject = await Controller.GetSubjects();

    }
}