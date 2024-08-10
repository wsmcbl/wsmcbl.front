using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Shared;

namespace wsmcbl.src.View.Accounting;

public class TariffProfiles : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; } 
    
    protected string? errorMesage { get; set; }
    
    protected List<StudentEntity>? students;
    
    protected override async Task OnInitializedAsync()
    {
       var result = await Controller.getStudentList_a();

       if (result == null)
       {
           errorMesage = "asdl";
           StateHasChanged();
       }
       else
       {
           students = result;
       }
    }
}