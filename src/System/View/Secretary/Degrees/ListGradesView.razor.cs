using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
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
    [Inject] protected Navigator Navigator { get; set; } = null!;

    protected bool tabsCreated;
    protected List<DegreeEntity>? DegreesList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            DegreesList = await Controller.GetDegreeList();
        }
        catch (Exception e)
        {
            Notificator.ShowError("Error", $"Obtuvimos algunos errores.\n Mensaje: {e}");
        }
        
    }
    
    protected async Task OpenModal(string gradeId)
    {
        GradeId = gradeId;
        await Navigator.ShowModal("confGrade");
    }
    
    protected void ViewGrade(string gradeId)
    {
        GradeId = gradeId;
        Navigator.ToPage($"/secretary/grades/configuration/{gradeId}/1");
    }
    
    protected async Task CreateTabs(string GradeId, int numberOfTabs)
    {
        if (numberOfTabs is < 1 or >= 7)
        {
            await Notificator.ShowWarning("Advertencia", "El número máximo de secciones es 7");
            return;
        }
        
        EnrollmentEntity Default = new();
        var response = await Controller.CreateEnrollments(GradeId, numberOfTabs, Default);
            
        if (response == Default)
        {
            return;
        }

        await Navigator.HideModal("confGrade");
        Navigator.ToPage($"/secretary/grades/configuration/{GradeId}/{SecctionsNumber}");
    }
}
