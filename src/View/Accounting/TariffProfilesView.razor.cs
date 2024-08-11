using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Accounting;

public class TariffProfiles : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    
    protected ICollection<StudentEntity>? StudentList;
    
    protected override async Task OnInitializedAsync()
    {
        StudentList = await Controller.GetStudentList();
    }
}