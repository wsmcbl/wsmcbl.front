namespace wsmcbl.front.Models.Accounting;

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


    public double getDebBalanceByTariff(int tariffId)
    {
        var tariffResult = paymentHistory.FirstOrDefault(t => t.TariffId == tariffId);

        if (tariffResult == null || tariffResult.DebtBalance == 0)
        {
            return - 1;
        }

        return tariffResult.DebtBalance;
    }

    public bool hasDebt(int tariffId)
    {
        var dto = paymentHistory.FirstOrDefault(p => p.TariffId == tariffId);
        
        return dto != null && dto.DebtBalance > 0;
    }
}