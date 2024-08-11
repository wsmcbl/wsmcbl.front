using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController
{
    private readonly ApiConsumer Consumer;

    public CollectTariffController(ApiConsumer consumer)
    {
        Consumer = consumer;

        cashier = new CashierEntity("caj-ktinoco");
    }

    public async Task<List<TariffDto>> GetTariffList(string key, string value)
    {
        var resource = $"tariffs/search?q={key}:{value}";
        
        List<TariffDto> defaultResult = [];
        return await Consumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }

    public async Task<List<TariffDto>> GetTariffsOverdue(string value)
    {
        // response.ReasonPhrase

        var resource = $"tariffs/search?q={value}";
        List<TariffDto> defaultResult = [];
        return await Consumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }

    public async Task<string> SendPay()
    {
        var defaultResult = new AuxResult();
        var content = cashier.getTransaction();
        
        var result = await Consumer.PostAsync(Modules.Accounting, "transactions", content, defaultResult);

        return result.transactionId;
    }

    public async Task<InvoiceDto?> GetInvoice(string transactionId)
    {
        var resource = $"transactions/invoices/{transactionId}";
        var result = await Consumer.GetAsync<InvoiceDto?>(Modules.Accounting, resource, null);

        return result;
    }

    public async Task<bool> ActiveArrears(int tariffId)
    {
        var resource = $"arrears/{tariffId}";
        await Consumer.PutAsync(Modules.Accounting, resource, string.Empty);

        return true;
    }

    public async Task<List<StudentEntity>?> GetStudentList()
    {
        List<StudentEntity> defaultResult = [];
        return await Consumer.GetAsync(Modules.Accounting, "students", defaultResult);
    }

    public async Task<StudentEntity> GetStudent(string studentId)
    {
        var resource = $"students/{studentId}";
        StudentEntity defaultResult = new();
        return await Consumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }

    private readonly CashierEntity cashier;

    public void AddDetail(List<TariffModalDto> tariffs, bool isApplyArrears)
    {
        cashier.initTransaction();
        cashier.addDetail(tariffs, isApplyArrears);
    }

    public void SetStudent(StudentEntity studentEntity)
    {
        cashier.setStudent(studentEntity);
    }
}

public class AuxResult
{
    public string transactionId { get; set; } 
}