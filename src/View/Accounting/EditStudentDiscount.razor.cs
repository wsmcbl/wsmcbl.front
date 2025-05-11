using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting;

namespace wsmcbl.src.View.Accounting;

public partial class EditStudentDiscount : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Parameter] public EventCallback OnUpdated { get; set; } = default!;

    [Inject] private UpdateStudentAccountingController Controller { get; set; } = null!;
    [Inject] private Notificator Notificator { get; set; } = null!;
    [Inject] private Navigator Navigator { get; set; } = null!;
    
    private int DiscountId { get; set; }
    private UpdateDiscountDto dto { get; set; } = new();
    

    private async Task UpdateDiscount()
    {
        dto.studentId = StudentId;
        dto.discountId = DiscountId;

        var result= await Notificator.ShowAlertQuestion("Advertencia", "¿Está seguro de aplicar este descuento?",
                ("Sí","No"));
        
        if (!result)
        {
            await Navigator.HideModal("EditDiscountModal");
            return;
        }
        
        var response = await Controller.UpdateDiscount(dto);
        if (response)
        {
            await Notificator.ShowSuccess("Se ha actualizado el descuento correctamente.");
            await Navigator.HideModal("EditDiscountModal");
            await OnUpdated.InvokeAsync();
            StateHasChanged();
        }
    }
    
    private void SetDiscount(int value)
    {
        DiscountId = value;
    }
}