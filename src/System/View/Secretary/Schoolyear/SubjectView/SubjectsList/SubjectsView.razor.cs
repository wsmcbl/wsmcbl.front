using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Schoolyear.SubjectView.SubjectsList;

public partial class SubjectsView : BaseView
{
    [Inject] private CreateSubjectDataController Controller { get; set; } = null!;
    private List<SubjectDataEntity>? Subjects { get; set; }


    protected override async Task OnParametersSetAsync()
    {
        Subjects = await Controller.GetSubjectList();
    }
    protected override bool IsLoading()
    {
        return Subjects == null;
    }
}