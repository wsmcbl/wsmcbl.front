using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting;

public class TariffProfiles : BaseView
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