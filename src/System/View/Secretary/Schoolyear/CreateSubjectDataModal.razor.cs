using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.SchoolYears.Dto;

namespace wsmcbl.src.View.Secretary.SchoolYears;

public partial class CreateSubjectDataModal : ComponentBase
{
    [Parameter] public SchoolYearEntity? SchoolYearEntity { get; set; }
    [Inject] protected CreateSchoolyearController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    
    private List<DropDownItem> semesterItemList { get; set; } =
    [
        new() { Id = 1, Name = "Primer Semestre" },
        new() { Id = 2, Name = "Segundo Semestre" },
        new() { Id = 3, Name = "Ambos" }
    ];    
    private List<DropDownItem> degreeItemList = [];
    private SubjectDto SubjectNew = new();

    protected override Task OnParametersSetAsync()
    {
        var degreeId = 1;
        foreach (var item in SchoolYearEntity!.degreeList!)
        {
            degreeItemList.Add(new DropDownItem
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
        var response = await controller.CreateNewSubject(subject);
        if (response)
        {
            await Notificator!.ShowSuccess("Se ha creado la asignatura correctamente, recarge la página.");
        }
    }
}