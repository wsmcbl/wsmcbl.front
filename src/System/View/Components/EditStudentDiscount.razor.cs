using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.View.Components;

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

        var des= await Notificator.ShowAlertQuestion("Advertencia", "Esta seguro que desea aplicar esté descuento?",("Si","No"));
        if (des)
        {
            var response = await Controller.EditDiscount(EditDiscount);
            if (response)
            {
                await Notificator.ShowSuccess("Exito", "Descuento actualizado");
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