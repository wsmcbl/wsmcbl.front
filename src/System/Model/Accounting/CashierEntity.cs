using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Model.Accounting;

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

    private TransactionEntity transaction;
    public TransactionEntity getTransaction() => transaction;

    public void initTransaction()
    {
        transaction = new TransactionEntity
        {
            studentId = student.studentId,
            cashierId = cashierId,
            dateTime = DateTime.UtcNow,
            details = []
        };
    }
    
    public void addDetail(List<TariffModalDto> tariffs, bool isApplyArrear)
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