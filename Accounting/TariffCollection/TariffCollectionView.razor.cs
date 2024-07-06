using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Controllers;
using wsmcbl.front.dto.input;
using wsmcbl.front.model.accounting;

namespace wsmcbl.front.Accounting.TariffCollection;

public class TariffCollection : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    
    [Inject] protected TariffCollectionController controller { get; set; } = null!;
    [Inject] protected AlertService alertService { get; set; } = null!;
    [Inject] protected IJSRuntime JSRuntime { get; set; } = null!;


    protected double arrears { get; set;}
    protected double subtotal { get; set;}
    protected double discount { get; set;}
    protected double Total { get; set; }
    
    protected double amountToDivide { get; set; }
    private bool areArrearsApply { get; set; } = true;


    protected List<TariffDto>? tariffList { get; set; }
    private List<TariffModal>? tariffModalList { get; set; }
    protected List<TariffModal>? tariffsToPay { get; set; }
    
    protected StudentEntity? student { get; private set; }
    
    protected bool isLoading() => student == null || tariffList == null;
    
    protected override async Task OnParametersSetAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(StudentId))
            {
                throw new ArgumentException("Student ID is not valid");
            }

            await LoadStudent();
            
            tariffList = await controller.GetTariffs("student", StudentId!);

            tariffModalList = tariffList.Where(t => isNotPay(t.TariffId)).ToModalList(student!);
            
            ClearList();
        }
        catch (ArgumentException ae)
        {
            await alertService.AlertWarning(ae.Message);
        }
        catch
        {
            await alertService.AlertWarning("Obtuvimos problemas al cargar los datos del estudiante");
        }
    }
    
    private bool isNotPay(int tariffId) => !student!.hasPayments(tariffId) || student.getDebt(tariffId) != 0;
    
    protected void ClearList()
    {
        tariffsToPay = [];
        computeTotal();
        amountToDivide = 0;
    }

    private void computeTotal()
    {
        subtotal = 0;
        discount = 0;
        arrears = 0;
        
        foreach (var item in tariffsToPay!)
        {
            subtotal += item.Total;
            discount += item.Discount;
            arrears += item.Arrear;
        }
        
        Total = subtotal;
    }

    private async Task LoadStudent()
    {
        student = await controller.GetStudent(StudentId!);
        controller.setStudent(student);
    }
    
    protected void OnSelectItemChanged(ChangeEventArgs e, TariffDto tariff)
    {
        if(e.Value == null) 
            return;
        
        var isSelect = (bool)e.Value;
        var tariffModal = tariffModalList!.First(t => t.TariffId == tariff.TariffId);
        
        if (isSelect && !tariffsToPay!.Contains(tariffModal))
        {
            tariffsToPay.Add(tariffModal);
        }
        else
        {
            tariffsToPay!.Remove(tariffModal);
        }
    }
    
    protected void ExonerateArrears(ChangeEventArgs e)
    {
        areArrearsApply = (bool) e.Value!;
        
        Total += (areArrearsApply ? -1 : 1)*arrears;
    }
    
    protected async Task Pay()
    {
        controller.addDetail(tariffsToPay!, areArrearsApply);
        
        var result = await controller.SendPay();

        if (string.IsNullOrEmpty(result))
        {
            await alertService.AlertError("¡Error en el Pago!", "La transacción no se completó correctamente.");
            return;
        }
        
        await alertService.AlertSuccess("¡Pago Exitoso!", "La transacción se completó correctamente.");
        await JSRuntime.InvokeVoidAsync("window.open", $"/transactions/invoices/{result}", "_blank");
        
        await LoadStudent();
        ClearList();
        StateHasChanged();
    }
    
    protected async Task ConfirmTransaction()
    {
        var modal = tariffsToPay!.Count == 0 ? "middlePay" : "finistariff";

        computeTotal();   
        await JSRuntime.InvokeVoidAsync("showModal", modal);
    }
    
    protected void DistributePay()
    {
        if (amountToDivide <= 0)
            return;

        foreach (var item in getDebtTariffList())
        {
            if (item.Total > amountToDivide)
            {
                item.Total = Math.Round(amountToDivide, 1);
            }
            
            tariffsToPay!.Add(item);

            amountToDivide -= item.Total;

            if (amountToDivide == 0)
            {
                break;
            }
        }
        
        computeTotal();
    }

    private List<TariffModal> getDebtTariffList()
    {
        return tariffModalList!
            .Where(item => tariffList!.First(t => t.TariffId == item.TariffId).Type == 1)
            .ToList();
    }
}