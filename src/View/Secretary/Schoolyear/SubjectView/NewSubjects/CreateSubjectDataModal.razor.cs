using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView.NewSubjects;

public partial class CreateSubjectDataModal : ComponentBase
{
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    [Inject] protected CreateSubjectDataController controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private List<SubjectAreaEntity> AreaList { get; set; } = new();
    private SubjectDataEntity _subjectNew = new();
    private List<DropDownItem> semesterItemList { get; set; } =
    [
        new() { Id = 1, Name = "Primer Semestre" },
        new() { Id = 2, Name = "Segundo Semestre" },
        new() { Id = 3, Name = "Ambos" }
    ];    
    private List<DropDownItem> _degreeItemList = [];

    protected override async Task OnParametersSetAsync()
    {
       await GetDegreeList();
       AreaList = await controller.GetAreaList();
       _subjectNew.degreeDataId = _degreeItemList[0].Id;
       _subjectNew.areaId = AreaList.First().areaId;
       _subjectNew.semester = semesterItemList[0].Id;
    }

    private async Task GetDegreeList()
    {
        var degreeList = await controller.GetDegreeDataList();
        var degreeId = 1;
        _degreeItemList = new();
        foreach (var item in degreeList)
        {
            _degreeItemList.Add(new DropDownItem
            {
                Id = degreeId,
                Name = item.label
            });
            degreeId++;
        }
    }

    private async Task SaveNewSubject( )
    {
        var response = await controller.CreateSubjectData(_subjectNew);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha creado la asignatura correctamente.");
            await OnEditCompleted.InvokeAsync();
            return;
        }
        await Notificator.ShowError("Hubo un fallo al crear la asignatura.");
    }
}