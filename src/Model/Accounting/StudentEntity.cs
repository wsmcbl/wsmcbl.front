using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Model.Accounting;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public bool isActive { get; set; }
    public string? schoolyear { get; set; }
    public string enrollmentLabel { get; set; } = null!;
    public string educationalLevel { get; set; } = null!;
    public string tutor { get; set; } = null!;
    public decimal discount { get; set; }
    public int discountId { get; set; }

    public List<PaymentDto> paymentHistory { get; set; } = null!;
    public Paginator<DebtDto> data { get; set; } = null!;

    public decimal GetDebt(int tariffId)
    {
        return paymentHistory.First(t => t.tariffId == tariffId).debtBalance;
    }

    public bool HasPayments(int tariffId)
    {
        return paymentHistory.Find(p => p.tariffId == tariffId) != null;
    }
    
}