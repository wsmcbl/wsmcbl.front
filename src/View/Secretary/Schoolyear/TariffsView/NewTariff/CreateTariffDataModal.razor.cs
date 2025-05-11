using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Components.Dto;

namespace wsmcbl.src.View.Secretary.Schoolyear.TariffsView.NewTariff;

public partial class CreateTariffDataModal : ComponentBase
{
    [Inject] protected CreateTariffDataController controller { get; set; } = null!;
    [Parameter] public EventCallback OnEditCompleted { get; set; }
    [Inject] protected Navigator navigator { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    private bool isOverdue { get; set; }
    private DateOnly dueDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    private CreateTariffDto _tariff = new() { educationalLevel = 3 };
    private readonly List<DropDownItem>? _tariffTypeItemList =
    [
        new() { Id = 2, Name = "Matrícula" },
        new() { Id = 1, Name = "Mensualidad" }
    ];
    
    protected override Task OnParametersSetAsync()
    {
        isOverdue = _tariff.dueDate == null;
        return Task.CompletedTask;
    }

    private async Task CreateTariffData()
    
    { 
        if (!isOverdue && dueDate < DateOnly.FromDateTime(DateTime.Now))
        {
            await Notificator.ShowError("La fecha de vencimiento no puede ser anterior a la fecha actual.");
            return;
        }
        
        _tariff.dueDate = isOverdue ? null : dueDate.MapToDto();
        var response = await controller.CreateTariffData(_tariff);
        if (response != null)
        {
            await Notificator.ShowSuccess("Se ha registrado el arancel correctamente.");
            await navigator.HideModal("ModalNewTariff");
            await OnEditCompleted.InvokeAsync();
            _tariff = new() { educationalLevel = 3 };
        }
        else
        {
            await Notificator.ShowError( "Hubo un fallo al registrar el arancel.");
        }
    }
}