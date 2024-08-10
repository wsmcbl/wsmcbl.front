using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Service;
using wsmcbl.src.View.Shared;

namespace wsmcbl.src.View.Accounting;

public class TariffProfiles : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; }
    [Inject] protected ErrorService error { get; set; }
    
    protected ICollection<StudentEntity>? StudentList;
    
    protected override async Task OnInitializedAsync()
    {
        var result = await Controller.GetStudentList_Test();

        if (result != null)
            StudentList = result;
        else
        {
            StudentList = [];
            error.ShowError("Error inesperado.");
        }
            
    }
}