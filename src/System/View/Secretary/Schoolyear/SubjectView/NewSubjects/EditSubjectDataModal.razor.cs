using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView.NewSubjects;

public partial class EditSubjectDataModal : ComponentBase
{
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    [Parameter] public SubjectDataEntity editSubject { get; set; } = new();
    
    [Inject] protected CreateSubjectDataController controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; set; } = null!;
    private List<SubjectAreaEntity> AreaList { get; set; } = new();
    private List<DropDownItem> _degreeItemList = [];
    private List<DropDownItem> semesterItemList { get; set; } =
    [
        new() { Id = 1, Name = "Primer Semestre" },
        new() { Id = 2, Name = "Segundo Semestre" },
        new() { Id = 3, Name = "Ambos" }
    ];    
    
    protected override async Task OnParametersSetAsync()
    {
        await GetDegreeList();
        AreaList = await controller.GetAreaList();
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

    private async Task UpdateSubject()
    {
        var response = await controller.UpdateSubjectData(editSubject);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha actualizado la asignatura correctamente.");
            await Navigator.HideModal("ModalEditSubject");
            await OnEditCompleted.InvokeAsync();
            return;
        }
        await Notificator.ShowError("Hubo un fallo al actualizar la asignatura.");
    }
}