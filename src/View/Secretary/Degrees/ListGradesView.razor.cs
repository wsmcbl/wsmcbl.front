using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Degrees;

public class ListGrades : ComponentBase
{
    [Parameter]
    public int SecctionsNumber { get; set; }
    [Parameter]
    public string GradeId { get; set; }
    
    [Inject] protected IJSRuntime? JsRuntime { get; set; } 
    [Inject] protected CreateOfficialEnrollmentController? Controller { get; set; }
    [Inject] protected Notificator? Notificator { get; set; }

    protected bool tabsCreated;
    protected List<DegreeEntity>? Degrees { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Degrees = await Controller.GetDegreeList();
        }
        catch (Exception e)
        {
            Notificator.ShowError("Error", $"Obtuvimos algunos errores.\n Mensaje: {e}");
        }
        
    }
    
    protected async Task OpenModal(string gradeId)
    {
        GradeId = gradeId;
        await JsRuntime.InvokeVoidAsync("eval", "$('#confGrade').modal('show');");
    }
    
    protected async Task CreateTabs(string GradeId, int numberOfTabs)
    {
        if (numberOfTabs <= 0 && numberOfTabs >= 7)
        {
            Notificator.ShowWarning("Advertencia", "El numero maximo de secciones es 7");
            return;
        }
        
        EnrollmentEntity Default = new();
        var response = await Controller.CreateEnrollments(GradeId, numberOfTabs, Default);
        if (response == Default)
        {
            Notificator.ShowError("Obtubimos problemas al crear las secciones");
        }
        OpenNewTab();
    }
    
    protected async Task OpenNewTab()
    {
        await JsRuntime.InvokeVoidAsync("window.open", $"/secretary/grades/configuration/{GradeId}/{SecctionsNumber}", "_blank");
    }
}