using System.Text.Json;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.CollectTariffs.Dto;

namespace wsmcbl.src.Controller;

public class CollectTariffController
{
    private readonly ApiConsumer Consumer;

    public CollectTariffController(ApiConsumer consumer)
    {
        Consumer = consumer;
        cashier = new CashierEntity("caj-mmercado");
    }

    public async Task<List<TariffDto>> GetTariffListByStudentId(string studentId)
    {
        var resource = $"tariffs/search?q=student:{studentId}";
        List<TariffDto> defaultResult = [];
        return await Consumer.GetAsync(Modules.Accounting, resource, defaultResult);
    }
    
    public async Task<string> SendPay()
    {
        var defaultResult = new AuxResult();
        var content = cashier.getTransaction();
        var json = JsonSerializer.Serialize(content);

        
        var result = await Consumer.PostAsync(Modules.Accounting, "transactions", content, defaultResult);

        return result.transactionId;
    }
    

    public async Task<bool> ActiveArrears(int tariffId)
    {
        var resource = $"arrears/{tariffId}";
        return await Consumer.PutAsync(Modules.Accounting, resource, string.Empty);
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
    
    public void SetStudent(StudentEntity studentEntity)
    {
        cashier.setStudent(studentEntity);
    }
}

public class AuxResult
{
    public string transactionId { get; set; } 
}