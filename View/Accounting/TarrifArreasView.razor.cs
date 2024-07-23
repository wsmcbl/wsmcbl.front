using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using wsmcbl.front.Controller;
using wsmcbl.front.dto.input;
    
    
namespace wsmcbl.front.View.Accounting;

public partial class TarrifArreas : ComponentBase
{
    [Inject] protected CollectTariffController tariffcontroller { get; set; } = null!;
    [Inject] protected SweetAlertService Swal { get; set; } = null!;
    
    protected List<TariffDto> selectedTariffs = new List<TariffDto>();
    protected List<TariffDto> tariffs = null!;
    protected int applyArreas;
    
    protected override async Task OnInitializedAsync()
    {
        tariffs = await tariffcontroller.GetTariffsOverdue("state:overdue");
    }
    
    protected void OnSelectItemChanged(ChangeEventArgs e, TariffDto tariff)
    {
        if ((bool)e.Value)
        {
            if (!selectedTariffs.Contains(tariff))
            {
                selectedTariffs.Add(tariff);
                applyArreas = tariff.TariffId;
            }
        }
        else
        {
            if (selectedTariffs.Contains(tariff))
            {
                selectedTariffs.Remove(tariff);
            }
        }
    }
    
    protected async Task Confirm()
    {
        try
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¿Seguro que desea generar mora?",
                Text = "La mora se aplicara a todos los estudiantes que no hayan pagado a tiempo",
                ShowDenyButton = true,
                ConfirmButtonText = "Aplicar",
                DenyButtonText = "No guardar"
            });

            if (result.IsConfirmed)
            {
                var response = await tariffcontroller.ActiveArrears(applyArreas);

                if (response)
                {
                    await Swal.FireAsync("Mora generada correctamente!", "", "success");
                }
                else
                {
                    await Swal.FireAsync("Obtuvimos problemas al generar la mora", "", "info");
                }
            }
            else if (result.IsDenied)
            {
                await Swal.FireAsync("No se ha generado mora", "", "info");
            }
        }
        catch(Exception ex)
        {
            await Swal.FireAsync("Error", $"Ha ocurrido un error: {ex.Message}", "error");
        }    
    }
}