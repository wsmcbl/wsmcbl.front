using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Shared;

namespace wsmcbl.src.View.Accounting;

public class TariffProfiles : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; }
    
    protected ICollection<StudentEntity>? StudentList;
    
    protected override async Task OnInitializedAsync()
    {
        StudentList = await Controller.GetStudentList_Test();
    }
}