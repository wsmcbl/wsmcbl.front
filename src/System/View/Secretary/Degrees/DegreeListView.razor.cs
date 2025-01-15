using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class DegreeListView : BaseView
{
    [Parameter] public int SectionsNumber { get; set; }
    [Parameter] public string? degreeId { get; set; }
    [Inject] protected CreateEnrollmentController? createController { get; set; }
    [Inject] protected Notificator? Notificator { get; set; }
    [Inject] protected Navigator Navigator { get; set; } = null!;

    protected List<DegreeEntity>? DegreeList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            DegreeList = await createController!.GetDegreeList();
        }
        catch (Exception e)
        {
            await Notificator!.ShowError("Error", $"Obtuvimos algunos errores.\n Mensaje: {e}");
        }
        
    }
    
    protected async Task OpenModal(string value)
    {
        degreeId = value;
        await Navigator.ShowModal("confGrade");
    }
    
    protected void ViewGrade(string value)
    {
        degreeId = value;
        Navigator.ToPage($"/secretary/degrees/{degreeId}/enrollments/{SectionsNumber}");
    }
    
    protected async Task CreateEnrollments(string value, int quantity)
    {
        if (quantity is < 1 or >= 7)
        {
            await Notificator!.ShowWarning("Advertencia", "El número máximo de secciones es 7.");
            return;
        }

        degreeId = value;
        
        var response = await createController!.CreateEnrollments(degreeId, quantity);
        if (response == null)
        {
            return;
        }

        await Navigator.HideModal("confGrade");
        Navigator.ToPage($"/secretary/degrees/{degreeId}/enrollments");
    }

    protected override bool IsLoading()
    {
        return DegreeList == null;
    }
}
