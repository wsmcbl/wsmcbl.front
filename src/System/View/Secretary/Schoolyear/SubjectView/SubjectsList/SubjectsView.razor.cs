using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView.SubjectsList;

public partial class SubjectsView : BaseView
{
    [Inject] private CreateSubjectDataController Controller { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    private List<SubjectDataEntity>? Subjects { get; set; }
    private List<DegreeDataEntity> DegreeList { get; set; } = new();
    private List<SubjectAreaEntity> AreaList { get; set; } = new();


    protected override async Task OnParametersSetAsync()
    {
        Subjects = await Controller.GetSubjectList();
        DegreeList = await Controller.GetDegreeDataList();
        AreaList = await Controller.GetAreaList();
    }
    protected override bool IsLoading()
    {
        return Subjects == null;
    }

    private async Task CreateNewSubject()
    {
        await Navigator.ShowModal("ModalNewSubject");
    }
}