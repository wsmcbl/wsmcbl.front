using wsmcbl.front.dto.Input;
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
            dateTime = DateTime.UtcNow,
            details = []
        };
    }
    
    public void addDetail(List<TariffModal> tariffs, bool isApplyArrear)
    {
        foreach (var item in tariffs)
        {
            if (!isApplyArrear)
            {
                item.Arrear = 0;
                item.computeTotal();
            }
            
            transaction.details.Add(new DetailDto
            {
                tariffId = item.TariffId,
                Amount = item.Total,
                applyArrear = isApplyArrear
            });
        }
    }
}