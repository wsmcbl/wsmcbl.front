using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting;

public partial class StudentListView : BaseView
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    
    private List<StudentEntity>? StudentList;
    
    protected override async Task OnInitializedAsync()
    {
        await loadStudentList();
    }
    
    private async Task loadStudentList()
    {
        StudentList = await Controller.GetStudentList();
    }

    protected override bool IsLoading()
    {
        return StudentList == null;
    }
}