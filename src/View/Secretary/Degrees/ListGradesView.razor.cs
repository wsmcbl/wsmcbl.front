using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Secretary.Degrees.Dto;

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
    [Inject] protected Navigator Navigator { get; set; }

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
    
    protected async Task ViewGrade(string gradeId)
    {
        GradeId = gradeId;
        await Navigator.ToPage($"/secretary/grades/configuration/{gradeId}/1");
    }
    
    protected async Task CreateTabs(string GradeId, int numberOfTabs)
    {
        if (numberOfTabs > 0 && numberOfTabs <= 7)
        {
            EnrollmentEntity Default = new();
            var response = await Controller.CreateEnrollments(GradeId, numberOfTabs, Default);
            if (response != Default)
            {
                OpenNewTab();
            }
            return;
        }
        Notificator.ShowWarning("Advertencia", "El numero maximo de secciones es 7");
    }
    
    protected async Task OpenNewTab()
    {
        await JsRuntime.InvokeVoidAsync("window.open", $"/secretary/grades/configuration/{GradeId}/{SecctionsNumber}", "_blank");
    }
}