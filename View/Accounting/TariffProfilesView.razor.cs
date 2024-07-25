using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.Model.Accounting;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Accounting;

public class TariffProfiles : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; } 
    
    protected List<StudentEntity>? students;
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            students = await Controller.getStudentList();
        }
        catch (Exception e)
        {
            AlertService.AlertError("Error", "Obtuvimos un error al cargar los datos.");
        }
    }
}