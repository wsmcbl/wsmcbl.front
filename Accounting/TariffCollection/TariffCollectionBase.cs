using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using wsmcbl.front.Controllers;
using wsmcbl.front.dto.Input;
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

    public List<TariffModal> selectedTariffs = new List<TariffModal>();
    public List<TariffModal> calculationTariffs = new List<TariffModal>();
    public List<TariffModal> requestTariffs = new List<TariffModal>();

    protected List<Tariff> tariffList;
    protected List<TariffModal> tariffModalList;

    protected StudentEntity student;
    
    [Parameter] 
    public string StudenId { get; set; }

    protected Exception loadingException;
    
    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(StudenId))
        {
            loadingException = new ArgumentException("Student ID is not valid");
            return;
        }

        try
        {
            await LoadStudent();
            tariffList = await controller.GetTariffs("student", StudenId);
            tariffModalList = getTariffModalList();
        }
        catch (Exception ex)
        {
            loadingException = ex;
        }
    }

    private List<TariffModal> getTariffModalList()
    {
        var list = new List<TariffModal>();

        foreach (var item in tariffList)
        {
            list.Add(item.ToModalItem(student));
        }

        return list;
    }

    private async Task LoadStudent()
    {
        student = await controller.GetStudent(StudenId);
        controller.setStudent(student);
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
        var tariffModal = tariffModalList.FirstOrDefault(t => t.TariffId == tariff.TariffId);
        
        if ((bool)e.Value)
        {
            if (!selectedTariffs.Contains(tariffModal))
            {
                calculationTariffs.Add(tariffModal);
            }
        }
        else
        {
            if (selectedTariffs.Contains(tariffModal))
            {
                selectedTariffs.Remove(tariffModal);
            }
        }
    }
    
    protected void DistributePay()
    {
        if (amountToDivide <= 0)
            return;

        foreach (var item in getDebtTariffList())
        {
            var total = item.Total;

            item.Total = (amountToDivide > total) ?  total : amountToDivide;

            requestTariffs.Add(item);

            amountToDivide -= item.Total;
            TOTAL += total;

            if (amountToDivide == 0)
            {
                break;
            }
        }
    }
    
    private List<TariffModal> getDebtTariffList()
    {
        var list = new List<TariffModal>();
        
        foreach (var item in tariffList)
        {
            if (item.Type != 1 || !student.hasDebt(item.TariffId))
            {
                continue;
            }

            list.Add(item.ToModalItem(student));
        }

        return list;
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
        if (requestTariffs.Count != 0)
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
        requestTariffs = new List<TariffModal>();
    }
}