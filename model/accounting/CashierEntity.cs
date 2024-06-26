using wsmcbl.front.Models.Accounting;
using wsmcbl.front.Models.Accounting.Output;

namespace wsmcbl.front.model.accounting;

public class CashierEntity
{
    private string cashierId;
    private double arrear = 0.1;
    
    public CashierEntity(string cashierId)
    {
        this.cashierId = cashierId;
    }  

    private StudentEntity student;
    public void setStudent(StudentEntity student)
    {
        this.student = student;
    }

    private TransactionDto transaction;
    public TransactionDto getTransaction => transaction;

    public void initTransaction()
    {
        transaction = new TransactionDto
        {
            studentId = student.studentId,
            cashierId = cashierId,
            dateTime = DateTime.UtcNow
        };

        transaction.details = new List<DetailDto>();
    }
    
    public void addDetail(List<Tariff> tariffs, bool isApplyArrear)
    {
        foreach (var item in tariffs)
        {
            transaction.details.Add(new DetailDto
            {
                tariffId = item.TariffId,
                Amount = getTotal(item),
                applyArrear = isApplyArrear
            });
        }
    }
    
    private double getTotal(Tariff item)
    {
        var total = item.Amount * (1 - student.discount);
        var arrearAmount = (item.IsLate) ? (1 + arrear) : 1;
        return Math.Round(total * arrearAmount);
    }
}