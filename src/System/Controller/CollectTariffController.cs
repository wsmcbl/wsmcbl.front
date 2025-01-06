using System.Text.Json;
using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private string CashierId { get; set; }

    public CollectTariffController(ApiConsumerWithNotificator apiConsumer)
    {
        _apiConsumer = apiConsumer;
        CashierId = "caj-eurbina";
    }

    public async Task<List<StudentEntity>?> GetStudentList()
    {
        List<StudentEntity> defaultResult = [];
        return await _apiConsumer.GetAsync(Modules.Accounting, "students", defaultResult);
    }

    private string? StudentId { get; set; }

    public async Task<StudentEntity> GetStudent(string studentId)
    {
        StudentId = studentId;
        var resource = $"students/{studentId}";
        StudentEntity defaultResult = new();
        return await _apiConsumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }

    public async Task<List<TariffEntity>> GetTariffListByStudentId(string? studentId)
    {
        var resource = $"tariffs/search?q=student:{studentId}";
        List<TariffDto> defaultResult = [];
        defaultResult = await _apiConsumer.GetAsync(Modules.Accounting, resource, defaultResult);

        return defaultResult.ToEntity();
    }

    public async Task<string> SendPay()
    {
        var defaultResult = new TransactionEntity();
        var result = await _apiConsumer.PostAsync(Modules.Accounting, "transactions", Transaction, defaultResult);
        return result.transactionId!;
    }

    public async Task<byte[]> GetInvoice(string transactionId)
    {
        var resource = $"documents/invoices/{transactionId}";
        return await _apiConsumer.GetPdfAsync(Modules.Accounting, resource);
    }

    public async Task<bool> CancelTransaction(string transactionId)
    {
        var resource = $"transactions/{transactionId}";
        var response = await _apiConsumer.PutAsync(Modules.Accounting, resource, transactionId);
        return response;
    }
    
    public async Task<List<DebtListDto>> GetDebitList(string studentId)
    {
        var resource = $"debts?studentId={studentId}";
        var defaultResult = new List<DebtListDto>();
        return await _apiConsumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }
    
    public async Task<bool> DebitTariff(DebDto dto)
    {
        var resource = "debts";
        var response = await _apiConsumer.PutAsync(Modules.Accounting, resource, dto);
        return response;
    }
    
    public async Task<bool> EditDiscount(EditDiscountDto editDiscountDto)
    {
        var response = await _apiConsumer.PutAsync(Modules.Accounting, "students", editDiscountDto);
        return response;
    }


    private TransactionEntity? Transaction { get; set; }

    public void BuildTransaction(List<DetailDto> transactionDetail)
    {
        Transaction = new TransactionEntity
        {
            transactionId = "",
            studentId = StudentId!,
            cashierId = CashierId,
            dateTime = DateTime.UtcNow,
            details = transactionDetail
        };
    }
}