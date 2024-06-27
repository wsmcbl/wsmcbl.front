﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Controllers;
using wsmcbl.front.model.accounting;
using wsmcbl.front.Models.Accounting;
using wsmcbl.front.Models.Accounting.Output;

namespace wsmcbl.front.Accounting.TariffCollection;

public class TariffCollectionBase : ComponentBase
{
    public TariffCollectionBase() { }
    
    [Inject]
    protected TariffCollectionController controller { get; set; }
    [Inject]
    protected AlertService alertService { get; set; }
    [Inject]
    protected NavigationManager navigationManager { get; set; }
    [Inject]
    protected IJSRuntime JSRuntime { get; set; }
    

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

    private bool applyArear = true;

    public List<Tariff> selectedTariffs = new List<Tariff>();
    public List<PaymentDto> calculationTariffs = new List<PaymentDto>();
    public List<Tariff> requestTariffs = new List<Tariff>();

    protected List<Tariff> fullTariffs;
    protected List<Tariff> debtTariffs;

    protected StudentEntity student;
    
    [Parameter] 
    public string StudenID { get; set; }

    protected Exception loadingException;
    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(StudenID))
        {
            loadingException = new ArgumentException("Student ID is not valid");
            return;
        }

        try
        {
            student = await controller.GetStudent(StudenID);
            controller.setStudent(student);
        }
        catch (Exception ex)
        {
            loadingException = ex;
        }

        fullTariffs = await controller.GetTariffs("student", StudenID);

        debtTariffs = new List<Tariff>();
        foreach (var item in fullTariffs)
        {
            if (item.Type != 1)//Fix 
            {
                continue;
            }

            var t = student.paymentHistory.FirstOrDefault(p => p.TariffId == item.TariffId);

            if (t != null && t.DebtBalance == 0)
            {
                continue;
            }

            debtTariffs.Add(item);
        }
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (loadingException == null)
        {
            return;
        }
        
        var error = await alertService.AlertWarning("Obtuvimos problemas al cargar los datos del estudiante");
    }
    
    protected void OnSelectItemChanged(ChangeEventArgs e, Tariff tariff)
    {
        if ((bool)e.Value)
        {
            if (!selectedTariffs.Contains(tariff))  
            {
                if (student.paymentHistory.Any(t => t.TariffId == tariff.TariffId && t.DebtBalance > 0)) //Existe un abono?
                {
                    double payment = student.paymentHistory.FirstOrDefault(t => t.TariffId == tariff.TariffId && t.DebtBalance > 0).DebtBalance;
                    PaymentDto paymentDto = tariff.ToPaymentDto(payment,0, 0);
                    calculationTariffs.Add(paymentDto);
                }
                else
                {
                    var desc = tariff.Amount * student.discount;
                    var mora = tariff.IsLate ? (tariff.Amount - desc) * taxArrears : 0;
                    PaymentDto paymentDto = tariff.ToPaymentDto(100, desc,mora);
                    calculationTariffs.Add(paymentDto);
                }
            }
        }
        else
        {
            if (selectedTariffs.Contains(tariff))
            {
                selectedTariffs.Remove(tariff);
                subtotal = subtotal - tariff.Amount;
                discount = 0;

                if (tariff.IsLate)
                {
                    arrears -= tariff.Amount * taxArrears;
                }
            }
        }
    }
    private double GetTotal(Tariff item)
    {
        var total = 0.0;
        var arrear = 0.0;
        
        if(student.paymentHistory.Any(t => t.TariffId == item.TariffId && t.DebtBalance > 0)) //Existe un abono?
        {
            item.Amount = student.paymentHistory.First(t => t.TariffId == item.TariffId && t.DebtBalance > 0).DebtBalance;
            total = item.Amount;
            arrear = 1;
        }
        else
        {
            total = item.Amount * (1 - student.discount);
            arrear = (item.IsLate) ? (1 + taxArrears) : 1;
        }
        
        return Math.Round(total * arrear);
    }
    protected void DistributePay()
    {
        if (amountToDivide <= 0)
            return;

        foreach (var item in debtTariffs)
        {
            var total = GetTotal(item);

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
    protected async Task Pay()
    {
        controller.addDetail(selectedTariffs, applyArear);
        var result = await controller.SendPay();
        
        if (result != string.Empty)
        {
            await alertService.AlertSuccess("¡Pago Exitoso!", "La transacción se completó correctamente.");
            await JSRuntime.InvokeVoidAsync("window.open", $"/transactions/invoices/{result}", "_blank");
            ReloadPage();
        }
        else
        {
            await alertService.AlertError("¡Error en el Pago!", "La transacción no se completó correctamente.");
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
    protected void ReloadPage()
    {
        navigationManager.NavigateTo(navigationManager.Uri, forceLoad: true);
    }
    protected void ExoMora(ChangeEventArgs e)
    {
        applyArear = (bool)e.Value;
        
        if (applyArear)
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