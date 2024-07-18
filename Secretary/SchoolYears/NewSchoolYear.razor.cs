using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controllers;
using wsmcbl.front.model.Secretary.Input;

namespace wsmcbl.front.Secretary.SchoolYears;

public  class NewSchoolYear_razor : ComponentBase
{
    [Inject] protected SecretaryController Controller { get; set; } = null!;
    [Parameter] public string? SchoolYearId { get; set; }
    
    protected List<GradeEntity>? ListGrade;
    protected List<Subject>? ListSubject;

    protected override async Task OnParametersSetAsync()
    {
        
        //Generar logica para al consultar la api con el parametro "new"
        //obtener el objeto SchoolYear con el Id generado y la lista de 
        //Grade y Subject
        ListGrade = await Controller.GetGrades();
        ListSubject = await Controller.GetSubjects();
    }
}