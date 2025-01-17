using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Academy;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Secretary.Degrees;

public partial class DegreeListView : BaseView
{
    [Parameter] public string? degreeId { get; set; }
    [Parameter] public int SectionsNumber { get; set; }

    [Inject] protected Navigator Navigator { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected CreateEnrollmentController controller { get; set; } = null!;
    
    private DegreeEntity? Degree { get; set; }
    private List<DegreeEntity>? DegreeList { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            DegreeList = await controller.GetDegreeList();
            StateHasChanged();
        }
        catch (Exception e)
        {
            await Notificator.ShowError($"Obtuvimos algunos errores.\n Mensaje: {e}");
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
            await Notificator.ShowInformation("El número máximo de secciones es 7.");
            return;
        }

        degreeId = value;
        Degree = await controller.CreateEnrollments(value, quantity);
        if (Degree == null)
        {
            await Notificator.ShowError("Hubo un fallo al crear las secciones.");
            return;
        }

        await Navigator.HideModal("confGrade");
        StateHasChanged();
        await Navigator.ShowModal("InitGrade");
    }

    protected override bool IsLoading()
    {
        return DegreeList == null;
    }
}
