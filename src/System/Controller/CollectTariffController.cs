using wsmcbl.src.Controller.Service;
using wsmcbl.src.Model.Accounting;
using wsmcbl.src.Utilities;
using wsmcbl.src.View.Accounting.TariffCollection;

namespace wsmcbl.src.Controller;

public class CollectTariffController
{
    private readonly ApiConsumerWithNotificator _apiConsumer;
    private readonly JwtClaimsService _JwtClaimsService;

    public CollectTariffController(ApiConsumerFactory apiConsumerFactory, JwtClaimsService jwtClaimsService)
    {
        _apiConsumer = apiConsumerFactory.WithNotificator;
        _JwtClaimsService = jwtClaimsService;
    }

    public async Task<Paginator<StudentEntity>> GetStudentList(PagedRequest pagedRequest)
    {
        Paginator<StudentEntity> defaultResult = new ();
        return await _apiConsumer.GetAsync(Modules.Accounting, 
            $"students{pagedRequest}", 
            defaultResult);
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
        var resource = $"students/{studentId}/tariffs"; 
        
        var result = await _apiConsumer.GetAsync(Modules.Accounting, resource, new List<TariffDto>());
        return result.Select(e => e.ToEntity()).ToList();
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
        return await _apiConsumer.PutAsync(Modules.Accounting, resource, transactionId);
    }
    
    public async Task<List<DebtDto>> GetDebtList(string studentId)
    {
        var resource = $"students/{studentId}/debts";
        var defaultResult = new List<DebtDto>();
        await _apiConsumer.GetAsync(Modules.Accounting, resource, defaultResult);
        
        return defaultResult;
    }
    
    public async Task<bool> ForgetDebt(string studentId, int tariffId, string authorizationToken)
    {
        var resource = $"students/{studentId}/debts?tariffId={tariffId}&authorizationToken={authorizationToken}";
        return await _apiConsumer.PutAsync<object>(Modules.Accounting, resource, null);
    }
    
    public async Task<bool> EditDiscount(EditDiscountDto editDiscountDto)
    {
        return await _apiConsumer.PutAsync(Modules.Accounting, "students", editDiscountDto);
    }


    private TransactionEntity? Transaction { get; set; }

    public async Task BuildTransaction(List<DetailDto> transactionDetail)
    {
        Transaction = new TransactionEntity
        {
            transactionId = "",
            studentId = StudentId!,
            cashierId = await GetCashierId(),
            dateTime = DateTime.UtcNow,
            details = transactionDetail
        };
    }

    private async Task<string> GetCashierId()
    {
        var value = await _JwtClaimsService.GetClaimAsync("roleid");
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InternalException("Este usuario no puede realizar esta acción.");
        }

        return value;
    }
}