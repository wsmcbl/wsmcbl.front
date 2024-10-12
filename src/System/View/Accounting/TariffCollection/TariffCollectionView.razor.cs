using Microsoft.AspNetCore.Components;
using wsmcbl.src.Controller;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;

namespace wsmcbl.src.View.Accounting.TariffCollection;

public class TariffCollection : ComponentBase
{
    [Parameter] public string? StudentId { get; set; }
    [Inject] protected CollectTariffController Controller { get; set; } = null!;
    [Inject] protected Notificator Notificator { get; set; } = null!;
    [Inject] protected Navigator Navigator { get; private set; } = null!;


    protected double Arrears { get; set; }
    protected double Subtotal { get; set; }
    protected double Discount { get; set; }
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
        if (string.IsNullOrEmpty(StudentId))
        {
            throw new InternalException("Student ID is not valid.");
        }

        await LoadStudent();

        TariffList = await Controller.GetTariffListByStudentId(StudentId);

        TariffModalList = TariffList.Where(t => isNotPay(t.TariffId)).ToModalList(StudentEntity!);

        ClearList();
    }

    private bool isNotPay(int tariffId) => 
        !StudentEntity!.HasPayments(tariffId) || StudentEntity.GetDebt(tariffId) != 0;

    protected void ClearList()
    {
        TariffsToPay = [];
        ComputeTotal();
        AmountToDivide = 0;
    }

    private void ComputeTotal()
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
    }

    protected void OnSelectItemChanged(ChangeEventArgs e, TariffDto tariff)
    {
        if (e.Value == null)
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
        AreArrearsApply = (bool)e.Value!;

        Total += (AreArrearsApply ? -1 : 1) * Arrears;
    }

    protected byte[]? InvoicePdf { get; set; }
    protected async Task MakePay()
    {
        Controller.BuildTransaction(TariffsToPay!, AreArrearsApply);

        var result = await Controller.SendPay();

        if (string.IsNullOrEmpty(result))
        {
            await Notificator.ShowError("¡Error en el Pago!", "La transacción no se completó correctamente.");
            return;
        }

        await Notificator.ShowSuccess("¡Pago Exitoso!", "La transacción se completó correctamente.");
        InvoicePdf = await Controller.GetInvoice(result);

        await LoadStudent();
        ClearList();
        StateHasChanged();
    }

    protected async Task ConfirmTransaction()
    {
        var modal = TariffsToPay!.Count == 0 ? "middlePay" : "finistariff";
        ComputeTotal();
        await Navigator.InvokeModal("showModal", modal);
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

        ComputeTotal();
    }

    private List<TariffModalDto> getDebtTariffList()
    {
        return TariffModalList!
            .Where(item => TariffList!.First(t => t.TariffId == item.TariffId).Type == 1)
            .ToList();
    }
}