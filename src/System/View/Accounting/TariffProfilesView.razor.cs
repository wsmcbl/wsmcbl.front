using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;

namespace wsmcbl.src.View.Accounting;

public partial class TariffProfilesView : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    
    private ICollection<StudentEntity>? StudentList;
    
    protected override async Task OnInitializedAsync()
    {
        StudentList = await Controller.GetStudentList();
    }

    private bool IsLoad()
    {
        return StudentList == null;
    }
}