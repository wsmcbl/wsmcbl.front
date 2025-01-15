using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Degrees;

public class ListGrades : ComponentBase
{
    [Parameter] public int SectionsNumber { get; set; }
    [Parameter] public string? GradeId { get; set; }
    [Inject] protected CreateEnrollmentController? createController { get; set; }
    [Inject] protected Notificator? Notificator { get; set; }
    [Inject] protected Navigator Navigator { get; set; } = null!;

    protected List<DegreeEntity>? DegreesList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            DegreesList = await createController!.GetDegreeList();
        }
        catch (Exception e)
        {
            await Notificator!.ShowError("Error", $"Obtuvimos algunos errores.\n Mensaje: {e}");
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
    
    protected async Task CreateTabs(string gradeId, int numberOfTabs)
    {
        if (numberOfTabs is < 1 or >= 7)
        {
            await Notificator!.ShowWarning("Advertencia", "El número máximo de secciones es 7");
            return;
        }
        
        EnrollmentEntity Default = new();
        var response = await createController!.CreateEnrollments(gradeId, numberOfTabs, Default);
        if (response == Default)
        {
            return;
        }

        await Navigator.HideModal("confGrade");
        Navigator.ToPage($"/secretary/grades/configuration/{gradeId}/{SectionsNumber}");
    }
}
