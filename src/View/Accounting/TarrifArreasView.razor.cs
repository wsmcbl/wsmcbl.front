using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.View.Accounting.TariffCollection;
using wsmcbl.src.View.Shared;

namespace wsmcbl.src.View.Accounting;

public partial class TarrifArreas : ComponentBase
{
    [Inject] protected CollectTariffController Controller { get; set; }
    [Inject] protected SweetAlertService Swal { get; set; }
    [Inject] protected AlertService AlertService { get; set; }
    
    protected List<TariffDto> SelectedTariffs = new List<TariffDto>();
    protected List<TariffDto> Tariffs = null!;
    protected int ApplyArreas;

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Tariffs = await Controller.GetTariffsOverdue("state:overdue");
        }
        catch (Exception e)
        {
            AlertService.AlertError("Error al cargar los datos", $"{e}");
            
        }
    }
    
    protected void OnSelectItemChanged(ChangeEventArgs e, TariffDto tariff)
    {
        if ((bool)e.Value)
        {
            if (!SelectedTariffs.Contains(tariff))
            {
                SelectedTariffs.Add(tariff);
                ApplyArreas = tariff.TariffId;
            }
        }
        else
        {
            if (SelectedTariffs.Contains(tariff))
            {
                SelectedTariffs.Remove(tariff);
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
                var response = await Controller.ActiveArrears(ApplyArreas);

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