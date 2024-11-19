using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class ModalNewSubject : ComponentBase
{
    [Parameter] public SchoolYearEntity? SchoolYearEntity { get; set; }
    [Inject] protected CreateOfficialEnrollmentController? Controller { get; set; }
    [Inject] protected Notificator? Notificator { get; set; }
    
    protected List<DropdownList> DropdownSemesterLists =
    [
        new DropdownList { Id = 1, Nombre = "Primer Semestre" },
        new DropdownList { Id = 2, Nombre = "Segundo Semestre" },
        new DropdownList { Id = 3, Nombre = "Ambos" }
    ];    
    protected List<DropdownList> DropdownDegreeLists = [];
    protected SubjectDto SubjectNew = new();

    protected override Task OnParametersSetAsync()
    {
        var degreeId = 1;
        foreach (var item in SchoolYearEntity!.degreeList!)
        {
            DropdownDegreeLists.Add(new DropdownList
            {
                Id = degreeId,
                Nombre = item.label
            });
            degreeId++;
        }

        return Task.CompletedTask;
    }


    protected async Task SaveNewSubject(SubjectDto subject)
    {
        var response = await Controller!.CreateNewSubject(subject);
        if (response)
        {
            await Notificator!.ShowSuccess("Éxito", "Asignatura creada correctamente, recarge la página");
        }
    }
    
    
    
}