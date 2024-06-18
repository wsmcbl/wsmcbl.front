using wsmcbl.front.Models.Accounting;

namespace wsmcbl.front.Accounting.TariffCollection;

using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Controllers;
using wsmcbl.front.Models.Accounting.Input;
using wsmcbl.front.Models.Accounting.Output;

public class TariffCollection
{
    public TariffCollection()
    {
    }
    
    protected TariffCollectionController controller;
    protected SweetAlertService Swal;
    protected NavigationManager navigationManager;
    protected IJSRuntime JSRuntime;

    public TariffCollection(TariffCollectionController controller, SweetAlertService Swal, NavigationManager navigationManager, IJSRuntime JSRuntime)
    {
        this.controller = controller;
        this.Swal = Swal;
        this.navigationManager = navigationManager;
        this.JSRuntime = JSRuntime;
    }
    
    //Se obtendra de un end-point en el futuro
    public double taxArrears = 0.10;
    protected double amountToDivide { get; set; }
    public double TOTAL = 0.0;

    //Usados para pagos enteros de un arancel.
    public double arrears = 0.0;
    public double subtotal = 0.0;
    public double discount = 0.0;

    //Usados para el calculo de abonos.
    public double arrears2 = 0.0;
    public double subtotal2 = 0.0;
    public double discount2 = 0.0;

    //Lista de tarifas a pagar en pagos completos
    public List<Tariff> selectedTariffs = new List<Tariff>();

    //Lista de tarifas a abonar
    public List<Tariff> requestTariffs = new List<Tariff>();

    protected List<Tariff> fullTariffs;
    protected List<Tariff> debtTariffs;

    protected StudentEntity student;

    [Parameter] public string StudenID { get; set; }

    protected Exception loadingException;
    
    protected void OnSelectItemChanged(ChangeEventArgs e, Tariff tariff)
    {
        if ((bool)e.Value)
        {
            if (!selectedTariffs.Contains(tariff))
            {
                selectedTariffs.Add(tariff);
                subtotal = subtotal + tariff.Amount;

                var discountAmount = tariff.Amount * student.discount;
                discount += tariff.Amount - discountAmount;

                if (tariff.IsLate)
                {
                    arrears += tariff.Amount * taxArrears;
                }
            }
        }
        else
        {
            if (selectedTariffs.Contains(tariff))
            {
                selectedTariffs.Remove(tariff);
                subtotal = subtotal - tariff.Amount;
                discount -= student.discount;

                if (tariff.IsLate)
                {
                    arrears -= tariff.Amount * taxArrears;
                }
            }
        }
    }

    protected void DistributePay()
    {
        if (amountToDivide <= 0)
            return;

        foreach (var item in debtTariffs)
        {
            var total = item.Amount * (1 - student.discount);
            
            var arrear = (item.IsLate) ? taxArrears : 0.0;
            total = Math.Round(total*(1 + arrear));

            item.Amount = (amountToDivide < total) ? amountToDivide : total; 

            requestTariffs.Add(item);

            amountToDivide -= item.Amount;
            TOTAL += total;

            if (amountToDivide == 0)
            {
                break;
            }
        }
    }

    protected async Task MultiPay()
    {
        var transaction = new Models.Accounting.Output.TransactionDto()
        {
            cashierId = "caj-ktinoco",
            studentId = StudenID,
            dateTime = DateTime.UtcNow,
        };

        var result = await controller.SendPay(transaction);
        if (result != string.Empty)
        {
            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¡Pago Exitoso!",
                Text = "La transacción se completó correctamente.",
                Icon = SweetAlertIcon.Success
            });

            await JSRuntime.InvokeVoidAsync("window.open", $"/transactions/invoices/{result}", "_blank");

            ReloadPage();
        }
        else
        {
            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "¡Error en el Pago!",
                Text = "La transacción no se completó correctamente.",
                Icon = SweetAlertIcon.Error
            });
        }
    }

    protected async Task ConfirmTransaction()
    {
        if (selectedTariffs.Count == 0)
        {
            TOTAL = subtotal2 + arrears2 - discount2;
            await JSRuntime.InvokeVoidAsync("showModal", "middlePay");
        }
        else
        {
            TOTAL = subtotal + arrears - discount;
            await JSRuntime.InvokeVoidAsync("showModal", "finistariff");
        }
    }

    void ReloadPage()
    {
        navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
    }

    protected void ExoMora(ChangeEventArgs e)
    {
        if ((bool)e.Value)
        {
            TOTAL -= arrears;
        }
        else
        {
            TOTAL += arrears;
        }
    }


    protected void CleanRequestList()
    {
        requestTariffs = new List<Tariff>();
    }
}