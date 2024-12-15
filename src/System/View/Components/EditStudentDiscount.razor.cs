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
    private int DiscountId { get; set; }
    private EditDiscountDto EditDiscount { get; set; } = new EditDiscountDto();
    

    private async Task UpdateDiscount()
    {
        EditDiscount.studentId = StudentId;
        EditDiscount.discountId = DiscountId;
        var responsde = await Controller.EditDiscount(EditDiscount);
        if (responsde)
        {
            await Notificator.ShowSuccess("Exito", "Descuento actualizado");
        }
    }
    
    private void SetDiscount(int value)
    {
        DiscountId = value;
    }
}