using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Secretary;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Secretary.Schoolyear.AreaView;

public partial class AreaEditModal : ComponentBase
{
    [Parameter] public EventCallback onEditComplete { get; set; } 
    [Inject] private CreateSubjectDataController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    [Parameter] public SubjectAreaEntity Area { get; set; } = new();

    private async Task EditArea()
    {
        var response = await Controller.UpdateArea(Area.areaId, Area.name);
        if (response)
        {
            await Notificator.ShowSuccess("Hemos actualizado correctamente el area");
            await onEditComplete.InvokeAsync();
            await Navigator.HideModal("ModalEditArea");
            return;
        }
        await Notificator.ShowError("Hubo un fallo al actualizar el area");
    }
}