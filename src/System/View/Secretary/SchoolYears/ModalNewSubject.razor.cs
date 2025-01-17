using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class ModalNewSubject : ComponentBase
{
    [Parameter] public SchoolYearEntity? SchoolYearEntity { get; set; }
    [Inject] protected CreateSchoolYearController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    
    private List<DropdownList> DropdownSemesterLists =
    [
        new DropdownList { Id = 1, Name = "Primer Semestre" },
        new DropdownList { Id = 2, Name = "Segundo Semestre" },
        new DropdownList { Id = 3, Name = "Ambos" }
    ];    
    private List<DropdownList> DropdownDegreeLists = [];
    private SubjectDto SubjectNew = new();

    protected override Task OnParametersSetAsync()
    {
        var degreeId = 1;
        foreach (var item in SchoolYearEntity!.degreeList!)
        {
            DropdownDegreeLists.Add(new DropdownList
            {
                Id = degreeId,
                Name = item.label
            });
            degreeId++;
        }

        return Task.CompletedTask;
    }

    private async Task SaveNewSubject(SubjectDto subject)
    {
        var response = await controller!.CreateNewSubject(subject);
        if (response)
        {
            await Notificator!.ShowSuccess("Éxito", "Asignatura creada correctamente, recarge la página");
        }
    }
}