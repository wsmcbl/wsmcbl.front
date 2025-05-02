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
    [Inject] protected Notificator? Notificator { get; set; }
    private bool isOverdue { get; set; }
    private DateOnly dueDate { get; set; }

    private readonly CreateTariffDto _tariff = new();
    private readonly List<DropDownItem>? _tariffTypeItemList =
    [
        new() { Id = 2, Name = "Matrícula" },
        new() { Id = 1, Name = "Mensualidad" }
    ];
    private readonly List<DropDownItem> _modalityItemList =
    [
        new() { Id = 1, Name = "Preescolar" },
        new() { Id = 2, Name = "Primaria" },
        new() { Id = 3, Name = "Secundaria" }
    ];
    private async Task CreateTariffData()
    {
        _tariff.dueDate = isOverdue ? null : dueDate.MapToDto();
        var response = await controller.CreateTariffData(_tariff);
        if (response != null)
        {
            await Notificator!.ShowSuccess("Se ha registrado el arancel correctamente.");
            await navigator.HideModal("ModalNewTariff");
            await OnEditCompleted.InvokeAsync();
        }
        else
        {
            await Notificator!.ShowError( "Hubo un fallo al registrar el arancel.");
        }
    }
}