using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class DegreeListView
{
    [Parameter] public int SectionsNumber { get; set; }
    [Parameter] public string? degreeId { get; set; }
    [Inject] protected CreateEnrollmentController? createController { get; set; }
    [Inject] protected Notificator? Notificator { get; set; }
    [Inject] protected Navigator Navigator { get; set; } = null!;

    private List<DegreeEntity>? DegreeList { get; set; }
    protected DegreeEntity? Degree { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            DegreeList = await createController!.GetDegreeList();
            StateHasChanged();
        }
        catch (Exception e)
        {
            await Notificator!.ShowError("Error", $"Obtuvimos algunos errores.\n Mensaje: {e}");
        }
        
    }

    private async Task CreateEnrollmentModal(string value)
    {
        degreeId = value;
        await Navigator.ShowModal("confGrade");
    }

    private void ViewGrade(string value)
    {
        degreeId = value;
        Navigator.ToPage($"secretary/degrees/{degreeId}/enrollments");
        
    }

    private async Task CreateEnrollments(string value, int quantity)
    {
        if (quantity is < 1 or >= 7)
        {
            await Notificator!.ShowWarning("Advertencia", "El número máximo de secciones es 7.");
            return;
        }

        degreeId = value;
        Degree = await createController!.CreateEnrollments(value, quantity);
        if (Degree == null)
        {
            await Notificator!.ShowError("Error", "No pudimos crear las secciones");
            return;
        }

        await Navigator.HideModal("confGrade");
        StateHasChanged();
        Navigator.ToPage($"/secretary/degrees/{degreeId}/enrollments/initialize");
    }

    private bool IsLoading()
    {
        return DegreeList == null;
    }
}
