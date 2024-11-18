using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public partial class TariffCollectionView : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; private set; } = null!;
    
    protected List<TariffEntity>? TariffList { get; set; }
    protected List<TariffEntity>? TariffsToPay { get; set; }

    
    protected StudentEntity? Student { get; private set; }
    protected bool IsLoading() => Student == null || TariffList == null;
    protected double EstimateTotal { get; set; }

    
    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(StudentId))
        {
            throw new InternalException("Student ID is not valid.");
        }

        await LoadStudent();
        TariffList = await Controller.GetTariffListByStudentId(StudentId);
        TariffList.UpdateAmounts(Student!);
        
        InvoicePdf = [];
        TariffsToPay = [];
    }
    
    private async Task LoadStudent()
    {
        Student = await Controller.GetStudent(StudentId!);
    }
    
    private void OnSelectItemChanged(ChangeEventArgs e, TariffEntity tariff)
    {
        if (e.Value == null)
            return;

        var isSelect = (bool)e.Value;
        var tariffModal = TariffList!.First(t => t.TariffId == tariff.TariffId);
        
        var hasDebt = Student!.HasPayments(tariff.TariffId);
        var amount = hasDebt && Student.GetDebt(tariff.TariffId) != 0 ? Student.GetDebt(tariff.TariffId) : tariff.Amount;
        tariffModal.Amount = amount;
        
        if (isSelect && !TariffsToPay!.Contains(tariffModal))
        {
            EstimateTotal += tariffModal.Amount;
            TariffsToPay.Add(tariffModal);
        }
        else
        {
            TariffsToPay!.Remove(tariffModal);
            EstimateTotal -= tariffModal.Amount;
        }
    }

    private async Task OpenModal()
    {
        await Navigator.ShowModal("PaymentView");
    }
    
    private void HandleKeyDown(KeyboardEventArgs e)
    {
        if (!TariffsToPay.Any())
        {
            return;
        }
        
        if (e.Key == "Enter" && e.CtrlKey)
        {
            OpenModal();
        }
    }
    
    private byte[]? InvoicePdf { get; set; }
    private async Task PayTariffs(List<DetailDto> detail)
    {
        Controller.BuildTransaction(detail);

        var result = await Controller.SendPay();

        if (string.IsNullOrEmpty(result))
        {
            await Notificator.ShowError("¡Error en el Pago!", "La transacción no se completó.");
            return;
        }
        await Notificator.ShowSuccess("¡Pago Exitoso!", $"La transacción se completó correctamente.");
        await Navigator.HideModal("PaymentView");
        
        InvoicePdf = await Controller.GetInvoice(result);
        await Navigator.ShowPdfModal();
        
        await LoadStudent();
        TariffsToPay = [];
        StateHasChanged();
    }

    private string getDiscountFormat()
    {
        return Student == null ? "0 %" : $"{Student.discount * 100:0.00} %";
    }
}
