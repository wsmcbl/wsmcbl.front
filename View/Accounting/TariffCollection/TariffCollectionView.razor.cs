using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Controller;
using wsmcbl.front.Model.Accounting;
using wsmcbl.front.View.Shared;

namespace wsmcbl.front.View.Accounting.TariffCollection;

public class TariffCollection : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected AlertService AlertService { get; set; } = null!;
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;


    protected double Arrears { get; set;}
    protected double Subtotal { get; set;}
    protected double Discount { get; set;}
    protected double Total { get; set; }
    
    protected double AmountToDivide { get; set; }
    private bool AreArrearsApply { get; set; } = true;

 
    protected List<TariffDto>? TariffList { get; set; }
    private List<TariffModalDto>? TariffModalList { get; set; }
    protected List<TariffModalDto>? TariffsToPay { get; set; }
    
    protected StudentEntity? StudentEntity { get; private set; }
    
    protected bool IsLoading() => StudentEntity == null || TariffList == null;
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(StudentId))
            {
                throw new ArgumentException("Student ID is not valid");
            }

            await LoadStudent();
            
            TariffList = await Controller.GetTariffs("student", StudentId!);

            TariffModalList = TariffList.Where(t => isNotPay(t.TariffId)).ToModalList(StudentEntity!);
            
            ClearList();
        }
        catch (ArgumentException ae)
        {
            await AlertService.AlertWarning("Advertencia",ae.Message);
        }
        catch
        {
            await AlertService.AlertWarning("Advertencia","Obtuvimos problemas al cargar los datos del estudiante");
        }
    }
    
    private bool isNotPay(int tariffId) => !StudentEntity!.HasPayments(tariffId) || StudentEntity.GetDebt(tariffId) != 0;
    
    protected void ClearList()
    {
        TariffsToPay = [];
        computeTotal();
        AmountToDivide = 0;
    }

    private void computeTotal()
    {
        Subtotal = 0;
        Discount = 0;
        Arrears = 0;
        
        foreach (var item in TariffsToPay!)
        {
            Subtotal += item.Total;
            Discount += item.Discount;
            Arrears += item.Arrear;
        }
        
        Total = Subtotal;
    }

    private async Task LoadStudent()
    {
        StudentEntity = await Controller.GetStudent(StudentId!);
        Controller.setStudent(StudentEntity);
    }
    
    protected void OnSelectItemChanged(ChangeEventArgs e, TariffDto tariff)
    {
        if(e.Value == null) 
            return;
        
        var isSelect = (bool)e.Value;
        var tariffModal = TariffModalList!.First(t => t.TariffId == tariff.TariffId);
        
        if (isSelect && !TariffsToPay!.Contains(tariffModal))
        {
            TariffsToPay.Add(tariffModal);
        }
        else
        {
            TariffsToPay!.Remove(tariffModal);
        }
    }
    
    protected void ExonerateArrears(ChangeEventArgs e)
    {
        AreArrearsApply = (bool) e.Value!;
        
        Total += (AreArrearsApply ? -1 : 1)*Arrears;
    }
    
    protected async Task Pay()
    {
        Controller.addDetail(TariffsToPay!, AreArrearsApply);
        
        var result = await Controller.SendPay();

        if (string.IsNullOrEmpty(result))
        {
            await AlertService.AlertError("¡Error en el Pago!", "La transacción no se completó correctamente.");
            return;
        }
        
        await AlertService.AlertSuccess("¡Pago Exitoso!", "La transacción se completó correctamente.");
        await JsRuntime.InvokeVoidAsync("window.open", $"/transactions/invoices/{result}", "_blank");
        
        await LoadStudent();
        ClearList();
        StateHasChanged();
    }
    
    protected async Task ConfirmTransaction()
    {
        var modal = TariffsToPay!.Count == 0 ? "middlePay" : "finistariff";
        computeTotal();   
        await JsRuntime.InvokeVoidAsync("showModal", modal);
    }
    
    protected void DistributePay()
    {
        if (AmountToDivide <= 0)
            return;

        foreach (var item in getDebtTariffList())
        {
            if (item.Total > AmountToDivide)
            {
                item.Total = Math.Round(AmountToDivide, 1);
            }
            
            TariffsToPay!.Add(item);

            AmountToDivide -= item.Total;

            if (AmountToDivide == 0)
            {
                break;
            }
        }
        
        computeTotal();
    }

    private List<TariffModalDto> getDebtTariffList()
    {
        return TariffModalList!
            .Where(item => TariffList!.First(t => t.TariffId == item.TariffId).Type == 1)
            .ToList();
    }
}