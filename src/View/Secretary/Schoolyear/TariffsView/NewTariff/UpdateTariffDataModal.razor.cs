using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

public partial class UpdateTariffDataModal : ComponentBase
{
    [Inject] protected CreateTariffDataController controller { get; set; } = null!;
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    [Parameter] public CreateTariffDto tariffData { get; set; } = new();
    [Inject] protected Navigator navigator { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private DateOnly dueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private bool isOverdue { get; set; }
    private readonly List<DropDownItem> _tariffTypeItemList =
    [
        new() { Id = 2, Name = "Matrícula" },
        new() { Id = 1, Name = "Mensualidad" }
    ];
    protected override Task OnParametersSetAsync()
    {
        isOverdue = tariffData.dueDate == null;
        return Task.CompletedTask;
    }
    
    private async Task CompleteEdit()
    {
        if (!isOverdue && dueDate < DateOnly.FromDateTime(DateTime.Now))
        {
            await Notificator.ShowError("La fecha de vencimiento no puede ser anterior a la fecha actual.");
            return;
        }
        
        tariffData.dueDate = isOverdue ? null : dueDate.MapToDto();
        var response = await controller.UpdateTariffData(tariffData);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha actualizado el arancel correctamente.");
            await navigator.HideModal("ModalEditTariff");
            await OnEditCompleted.InvokeAsync();
            tariffData = new();
        }
        else
        {
            await Notificator.ShowError("Hubo un fallo al actualizar el arancel.");       
        }
    }
    
    
}