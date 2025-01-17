using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.View.Accounting;

public partial class EditStudentDiscount : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] private CollectTariffController Controller { get; set; } = default!;
    [Inject] private Notificator Notificator { get; set; } = default!;
    [Inject] private Navigator Navigator { get; set; } = default!;
    private int DiscountId { get; set; }
    private EditDiscountDto EditDiscount { get; set; } = new EditDiscountDto();
    

    private async Task UpdateDiscount()
    {
        EditDiscount.studentId = StudentId;
        EditDiscount.discountId = DiscountId;

        var result= await Notificator
            .ShowAlertQuestion("Advertencia", "¿Está seguro de aplicar este descuento?",
                ("Sí","No"));
        if (result)
        {
            var response = await Controller.EditDiscount(EditDiscount);
            if (response)
            {
                await Notificator.ShowSuccess("Se ha actualizado el descuento correctamente.");
                await Navigator.HideModal("EditDiscountModal");
                StateHasChanged();
                return;
            }
        }
        await Navigator.HideModal("EditDiscountModal");
    }
    
    private void SetDiscount(int value)
    {
        DiscountId = value;
    }
}