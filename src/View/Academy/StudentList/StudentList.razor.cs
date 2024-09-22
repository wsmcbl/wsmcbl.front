using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Academy.StudentList;

public class StudentList_view : BaseView
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    
    protected ICollection<StudentEntity>? StudentList;
    
    protected override async Task OnInitializedAsync()
    {
        StudentList = await Controller.GetStudentList();
    }

    protected override bool IsLoad()
    {
        return StudentList == null;
    }
}