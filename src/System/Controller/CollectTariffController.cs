using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController
{
    private readonly ApiConsumer Consumer;
    private string CashierId { get; set; }

    public CollectTariffController(ApiConsumer consumer)
    {
        Consumer = consumer;
        CashierId = "caj-mmercado";
    }
    
    public async Task<List<StudentEntity>?> GetStudentList()
    {
        List<StudentEntity> defaultResult = [];
        return await Consumer.GetAsync(Modules.Accounting, "students", defaultResult);
    }
    
    private string StudentId { get; set; }
    public async Task<StudentEntity> GetStudent(string studentId)
    {
        StudentId = studentId;
        var resource = $"students/{studentId}";
        StudentEntity defaultResult = new();
        return await Consumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }
    
    public async Task<List<TariffEntity>> GetTariffListByStudentId(string studentId)
    {
        var resource = $"tariffs/search?q=student:{studentId}";
        List<TariffDto> defaultResult = [];
        defaultResult = await Consumer.GetAsync(Modules.Accounting, resource, defaultResult);

        return defaultResult.ToEntity();
    }
    
    public async Task<string> SendPay()
    {
        var defaultResult = new TransactionEntity();
        var result = await Consumer
            .PostAsync(Modules.Accounting, "transactions", Transaction, defaultResult);

        return result!.transactionId!;
    }

    public async Task<byte[]> GetInvoice(string transactionId)
    {
        var resource = $"documents/invoices/{transactionId}";
        return await Consumer.GetPdfAsync(Modules.Accounting, resource);
    }
    
    
    private TransactionEntity Transaction { get; set; }
    public void BuildTransaction(List<DetailDto> transactionDetail)
    {
        Transaction = new TransactionEntity
        {
            transactionId = "",
            studentId = StudentId,
            cashierId = CashierId,
            dateTime = DateTime.UtcNow,
            details = transactionDetail
        }; 
    }
}
