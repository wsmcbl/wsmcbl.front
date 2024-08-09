using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Shared;

namespace wsmcbl.src.View.Accounting;

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