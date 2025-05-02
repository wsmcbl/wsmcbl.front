using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear.AreaView;

public partial class AreaView : BaseView
{ 
    [Inject] private CreateSubjectDataController Controller { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private List<SubjectAreaEntity>? AreaList { get; set; }
    private SubjectAreaEntity AreaEdit { get; set; } = new();

    protected override async Task OnParametersSetAsync()
    {
        AreaList = await Controller.GetAreaList();
    }
    private async Task OpenEditAreaModal(SubjectAreaEntity item)
    {
        AreaEdit = item;
        await Navigator.ShowModal("ModalEditArea");
    }
    protected override bool IsLoading()
    {
        return AreaList == null;
    }
}