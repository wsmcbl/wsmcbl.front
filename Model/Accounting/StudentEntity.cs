using wsmcbl.front.Models.Accounting;

namespace wsmcbl.front.Model.Accounting;

public class StudentEntity
{
    public string studentId { get; set; } = null!;
    public string fullName { get; set; } = null!;
    public string enrollmentLabel {get; set;} = null!;
    public string schoolyear { get; set; } = null!;
    public string tutor { get; set; } = null!;
    public double discount { get; set; }
    
    public bool isActive { get; set; }

    public List<PaymentDto> paymentHistory {get; set; } = null!;
    
    public double GetDebt(int tariffId)
    {
        return paymentHistory.First(t => t.TariffId == tariffId).DebtBalance;
    }

    public bool HasPayments(int tariffId)
    {
        return paymentHistory.FirstOrDefault(p => p.TariffId == tariffId) != null;
    }
}
