using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectData;

public partial class CreateSubjectDataModal : ComponentBase
{
    [Inject] protected CreateSubjectDataController controller { get; set; } = null!;
    [Inject] protected Notificator? Notificator { get; set; }
    
    private List<DropDownItem> semesterItemList { get; set; } =
    [
        new() { Id = 1, Name = "Primer Semestre" },
        new() { Id = 2, Name = "Segundo Semestre" },
        new() { Id = 3, Name = "Ambos" }
    ];    
    private List<DropDownItem> degreeItemList = [];
    private SubjectDataEntity SubjectNew = new();

    protected override async Task OnParametersSetAsync()
    {
        var degreeList = await controller.GetDegreeDataList();
        var degreeId = 1;
        foreach (var item in degreeList!)
        {
            degreeItemList.Add(new DropDownItem
            {
                Id = degreeId,
                Name = item.label
            });
            degreeId++;
        }
    }

    private async Task SaveNewSubject(SubjectDataEntity subject)
    {
        var response = await controller.CreateSubjectData(subject);
        if (response)
        {
            await Notificator!.ShowSuccess("Se ha creado la asignatura correctamente, recarge la página.");
        }
    }
}