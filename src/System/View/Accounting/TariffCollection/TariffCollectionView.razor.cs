using Microsoft.AspNetCore.Components;
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

    
    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(StudentId))
        {
            throw new InternalException("Student ID is not valid.");
        }

        InvoicePdf = [];
        await LoadStudent();

        TariffList = await Controller.GetTariffListByStudentId(StudentId);
        TariffList.ApplyDiscount(Student!);

        ClearList();
    }

    private async Task LoadStudent()
    {
        Student = await Controller.GetStudent(StudentId!);
    }

    protected void ClearList()
    {
        TariffsToPay = [];
    }
    
    protected void OnSelectItemChanged(ChangeEventArgs e, TariffEntity tariff)
    {
        if (e.Value == null)
            return;

        var isSelect = (bool)e.Value;
        var tariffModal = TariffList!.First(t => t.TariffId == tariff.TariffId);

        if (isSelect && !TariffsToPay!.Contains(tariffModal))
        {
            TariffsToPay.Add(tariffModal);
        }
        else
        {
            TariffsToPay!.Remove(tariffModal);
        }
    }

    protected byte[] InvoicePdf { get; set; }
    protected async Task MakePay()
    {
        var detail = TariffsToPay.MapToDto(true);
        Controller.BuildTransaction(detail);

        var result = await Controller.SendPay();

        if (string.IsNullOrEmpty(result))
        {
            await Notificator.ShowError("¡Error en el Pago!", "La transacción no se completó.");
            return;
        }
        await Notificator.ShowSuccess("¡Pago Exitoso!", $"La transacción se completó correctamente.");
        await Navigator.HideModal("finistariff");
        
        InvoicePdf = await Controller.GetInvoice(result);
        await Navigator.ShowPdfModal();
        
        await LoadStudent();
        ClearList();
        StateHasChanged();
    }

    protected async Task ConfirmTransaction()
    {
        await Navigator.ShowModal("PaymentView");
    }
}