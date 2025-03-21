using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public partial class TariffCollectionView : BaseView
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected CollectTariffController collectTariffController { get; set; } = null!;
    [Inject] protected ForgetDebtController forgetDebtController { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; private set; } = null!;

    private List<TariffEntity>? TariffList { get; set; }
    private List<TariffEntity>? TariffsToPay { get; set; }


    private StudentEntity? Student { get; set; }
    private double EstimateTotal { get; set; }
    private int TariffIdForDeb {get; set;}

    
    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(StudentId))
        {
            throw new InternalException("Student ID is not valid.");
        }

        await LoadStudent();
        InvoicePdf = [];
        TariffsToPay = [];
        EstimateTotal = 0;
    }
    
    private async Task LoadStudent()
    {
        Student = await collectTariffController.GetStudentById(StudentId!);
        
        Student.debtList = await forgetDebtController.GetDebtList(StudentId!);
        
        TariffList = await collectTariffController.GetTariffListByStudentId(StudentId);
        TariffList.UpdateAmounts(Student!);
    }
    
    private void OnSelectItemChanged(ChangeEventArgs e, TariffEntity tariff)
    {
        if (e.Value == null) return;

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
            
            if (EstimateTotal < 0) EstimateTotal = 0;
        }
    }

    private async Task OpenModal()
    {
        await Navigator.ShowModal("PaymentView");
    }

    private async Task OpenModalForEditDiscount()
    {
        await Navigator.ShowModal("EditDiscountModal");
    }
    
    private async Task HandleKeyDown(KeyboardEventArgs key)
    {
        if (TariffsToPay!.Count != 0 && key is { Key: "Enter", CtrlKey: true })
        {
            await OpenModal();
        }
    }
    
    private byte[]? InvoicePdf { get; set; }
    private async Task PayTariffs(List<DetailDto> detail)
    {
        await collectTariffController.BuildTransaction(StudentId!, detail);

        var result = await collectTariffController.SendPay();

        if (string.IsNullOrEmpty(result))
        {
            await Notificator.ShowError("¡Error en el Pago!", "La transacción no se completó.");
            return;
        }
        await Notificator.ShowSuccess("¡Pago Exitoso!", $"La transacción se completó correctamente.");
        await Navigator.HideModal("PaymentView");
        
        InvoicePdf = await collectTariffController.GetInvoice(result);
        await Navigator.ShowPdfModal();
        
        await LoadStudent();
        TariffsToPay = [];
        EstimateTotal = 0;
        StateHasChanged();
    }

    private string GetDiscountFormat()
    {
        return Student == null ? "0 %" : $"{Student.discount * 100:0.00} %";
    }

    private async Task OpenDebtModal(int tariffId)
    {
        TariffIdForDeb = tariffId;
        await Navigator.ShowModal("ForgetDebtModal");
    }

    protected override bool IsLoading()
    {
        return Student == null || TariffList == null;
    }
}
