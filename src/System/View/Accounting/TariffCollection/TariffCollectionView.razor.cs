using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using wsmcbl.src.Controller;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Base;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public partial class TariffCollectionView : BaseView
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected CollectTariffController collectTariffController { get; set; } = null!;
    [Inject] protected ForgetDebtController forgetDebtController { get; set; } = null!;
    [Inject] private GetSchoolYearServices GetSchoolYearServices { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; private set; } = null!;

    private List<TariffEntity>? TariffList { get; set; }
    private List<TariffEntity>? TariffsToPay { get; set; }
    private Dictionary<string, string>? SchoolYearLabels { get; set; }


    private StudentEntity? Student { get; set; }
    private decimal EstimateTotal { get; set; }
    private int TariffIdForDeb {get; set;}
    
    private PagedRequest Request { get; set; } = new();

    
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
        
        Student.data = await forgetDebtController.GetDebtList(StudentId!, Request);
        
        TariffList = await collectTariffController.GetTariffListByStudentId(StudentId);
        TariffList.UpdateAmounts(Student!);
        
        //Obtenemos los label de tododas las tarifas del student.
        var schoolYearsId = TariffList.Select(s => s.schoolyearId).Distinct().ToList();
        SchoolYearLabels = await GetSchoolYearServices.GetSchoolYearLabelsBatch(schoolYearsId);
    }
    
    private void OnSelectItemChanged(ChangeEventArgs e, TariffEntity tariff)
    {
        if (e.Value == null) return;

        var isSelect = (bool)e.Value;
        var tariffModal = TariffList!.First(t => t.tariffId == tariff.tariffId);
        
        var hasDebt = Student!.HasPayments(tariff.tariffId);
        var amount = hasDebt && Student.GetDebt(tariff.tariffId) != 0 ? Student.GetDebt(tariff.tariffId) : tariff.amount;
        tariffModal.amount = amount;
        
        if (isSelect && !TariffsToPay!.Contains(tariffModal))
        {
            EstimateTotal += tariffModal.amount;
            TariffsToPay.Add(tariffModal);
        }
        else
        {
            TariffsToPay!.Remove(tariffModal);
            EstimateTotal -= tariffModal.amount;
            
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

    private async Task DownLoadState()
    {
        await collectTariffController.GetAccountStatement(StudentId!);
    }
}
